using System.Collections.Generic;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Data;
using Project5LMS.Interfaces;

namespace Project5LMS.Services
{
    public class MembersService : IMembersService
    {
        private readonly IMemberRepository _memberRepository;

        public MembersService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository ?? throw new System.ArgumentNullException(nameof(memberRepository));
        }

        public MembersService(DatabaseContext dbContext) : this(new MemberRepository(dbContext))
        {
        }

        public Member GetMember(int memberId)
        {
            return _memberRepository.GetById(memberId);
        }

        public Member GetMemberByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return _memberRepository.GetByEmail(email);
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _memberRepository.GetAll();
        }

        public IEnumerable<Member> SearchMembers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Member>();

            return _memberRepository.Search(searchTerm);
        }

        public bool AddMember(Member member)
        {
            if (member == null || !member.IsValid())
                return false;

            return _memberRepository.Add(member);
        }

        public bool UpdateMember(Member member)
        {
            if (member == null || !member.IsValid())
                return false;

            return _memberRepository.Update(member);
        }

        public bool DeleteMember(int memberId)
        {
            if (memberId <= 0)
                return false;

            return _memberRepository.Delete(memberId);
        }

        public bool MemberExists(int memberId)
        {
            return _memberRepository.Exists(memberId);
        }

        public int GetActiveBorrowingCount(int memberId)
        {
            return _memberRepository.GetActiveBorrowingCount(memberId);
        }
    }
}
