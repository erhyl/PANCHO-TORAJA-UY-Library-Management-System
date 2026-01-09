using System.Collections.Generic;
using Project5LMS.Models;

namespace Project5LMS.Repositories
{

    public interface IMemberRepository
    {
        Member GetById(int memberId);
        Member GetByEmail(string email);
        IEnumerable<Member> GetAll();
        IEnumerable<Member> Search(string searchTerm);
        bool Add(Member member);
        bool Update(Member member);
        bool Delete(int memberId);
        bool Exists(int memberId);
        int GetActiveBorrowingCount(int memberId);
    }
}
