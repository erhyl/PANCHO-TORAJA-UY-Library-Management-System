using System.Collections.Generic;
using Project5LMS.Models;

namespace Project5LMS.Interfaces
{
    public interface IMembersService
    {
        Member GetMember(int memberId);
        Member GetMemberByEmail(string email);
        IEnumerable<Member> GetAllMembers();
        IEnumerable<Member> SearchMembers(string searchTerm);
        bool AddMember(Member member);
        bool UpdateMember(Member member);
        bool DeleteMember(int memberId);
        bool MemberExists(int memberId);
        int GetActiveBorrowingCount(int memberId);
    }
}
