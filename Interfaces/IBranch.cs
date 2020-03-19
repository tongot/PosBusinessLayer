

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
namespace AppDatabase
{
    public interface IBranch
    {
        void addBranch(Branch branch);
        void deleteBranch(int? id);
        Branch getBranchById(int id);
        IEnumerable<Branch> getAllBranches(string name);
        void UpdateBranch(Branch b);
        bool branchExist(string name);

    }
}
