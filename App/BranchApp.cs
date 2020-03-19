
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class BranchApp : IBranch
    {
        ApplicationContext _context = new ApplicationContext();
        public void addBranch(Branch branch)
        {
            _context.branches.Add(branch);
             _context.SaveChanges();
        }


        public bool branchExist(string name)
        {
            Branch b = _context.branches.Where(x => x.Name == name).FirstOrDefault();
            if (b == null)
            { return false; }
            return true;
        }

        public void deleteBranch(int? id)
        {
          if(id != null)
            {
                Branch b = _context.branches.Find(id);
                _context.branches.Remove(b);
                  _context.SaveChanges();
            }
        }

        public IEnumerable<Branch> getAllBranches(string name)
        {      
            return  _context.branches.Where(x=>x.Name.Contains(name)|| name==null).ToList();
        }

        public Branch getBranchById(int id)
        {
            return _context.branches.Find(id);
        }

        public void UpdateBranch(Branch b)
        {
            _context.Set<Branch>().AddOrUpdate(b);
            //_context.Entry(product).State = EntityState.Modified;
             _context.SaveChanges();
        }

    }
}
