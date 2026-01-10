using System;
using System.Collections.Generic;
using System.Linq;
namespace Project5LMS.Services
{
    public class LoginSecurityService
    {
        private static readonly Dictionary<string, LoginAttemptInfo> _loginAttempts = new Dictionary<string, LoginAttemptInfo>();
        private static readonly object _lockObject = new object();
        private const int MaxFailedAttempts = 5;
        private const int LockoutDurationMinutes = 15;
        private const int MaxAttemptsPerMinute = 3;
        public void RecordFailedAttempt(string email)
        {
            lock (_lockObject)
            {
                if (!_loginAttempts.ContainsKey(email))
                {
                    _loginAttempts[email] = new LoginAttemptInfo();
                }
                var attemptInfo = _loginAttempts[email];
                attemptInfo.FailedAttempts++;
                attemptInfo.LastAttemptTime = DateTime.Now;
                if (attemptInfo.FailedAttempts >= MaxFailedAttempts)
                {
                    attemptInfo.IsLockedOut = true;
                    attemptInfo.LockoutEndTime = DateTime.Now.AddMinutes(LockoutDurationMinutes);
                }
            }
        }
        public void RecordSuccessfulAttempt(string email)
        {
            lock (_lockObject)
            {
                if (_loginAttempts.ContainsKey(email))
                {
                    _loginAttempts[email].FailedAttempts = 0;
                    _loginAttempts[email].IsLockedOut = false;
                    _loginAttempts[email].LastAttemptTime = DateTime.Now;
                }
            }
        }
        public bool IsAccountLockedOut(string email, out string message)
        {
            message = string.Empty;
            lock (_lockObject)
            {
                if (!_loginAttempts.ContainsKey(email))
                {
                    return false;
                }
                var attemptInfo = _loginAttempts[email];
                if (attemptInfo.IsLockedOut && attemptInfo.LockoutEndTime.HasValue)
                {
                    if (DateTime.Now < attemptInfo.LockoutEndTime.Value)
                    {
                        var remainingMinutes = (int)Math.Ceiling((attemptInfo.LockoutEndTime.Value - DateTime.Now).TotalMinutes);
                        message = $"Account temporarily locked due to multiple failed login attempts. Please try again in {remainingMinutes} minute(s).";
                        return true;
                    }
                    else
                    {
                        attemptInfo.IsLockedOut = false;
                        attemptInfo.FailedAttempts = 0;
                        attemptInfo.LockoutEndTime = null;
                        return false;
                    }
                }
                return false;
            }
        }
        public bool IsRateLimited(string email, out string message)
        {
            message = string.Empty;
            lock (_lockObject)
            {
                if (!_loginAttempts.ContainsKey(email))
                {
                    return false;
                }
                var attemptInfo = _loginAttempts[email];
                var recentAttempts = attemptInfo.RecentAttemptTimes.Count(t => (DateTime.Now - t).TotalMinutes < 1);
                if (recentAttempts >= MaxAttemptsPerMinute)
                {
                    message = "Too many login attempts. Please wait a moment before trying again.";
                    return true;
                }
                attemptInfo.RecentAttemptTimes.Add(DateTime.Now);
                attemptInfo.RecentAttemptTimes.RemoveAll(t => (DateTime.Now - t).TotalMinutes >= 1);
                return false;
            }
        }
        public int GetRemainingAttempts(string email)
        {
            lock (_lockObject)
            {
                if (!_loginAttempts.ContainsKey(email))
                {
                    return MaxFailedAttempts;
                }
                var attemptInfo = _loginAttempts[email];
                return Math.Max(0, MaxFailedAttempts - attemptInfo.FailedAttempts);
            }
        }
        public void CleanupOldRecords()
        {
            lock (_lockObject)
            {
                var keysToRemove = _loginAttempts
                    .Where(kvp => !kvp.Value.IsLockedOut &&
                                 (DateTime.Now - kvp.Value.LastAttemptTime).TotalHours > 1)
                    .Select(kvp => kvp.Key)
                    .ToList();
                foreach (var key in keysToRemove)
                {
                    _loginAttempts.Remove(key);
                }
            }
        }
        private class LoginAttemptInfo
        {
            public int FailedAttempts { get; set; }
            public DateTime LastAttemptTime { get; set; }
            public bool IsLockedOut { get; set; }
            public DateTime? LockoutEndTime { get; set; }
            public List<DateTime> RecentAttemptTimes { get; set; } = new List<DateTime>();
        }
    }
}