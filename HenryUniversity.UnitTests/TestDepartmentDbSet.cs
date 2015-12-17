using HenryUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenryUniversity.UnitTests
{
    public class TestDepartmentDbSet : TestDbSet<Department>
    {
        public override Department Find(params object[] keyValues)
        {
            return this.SingleOrDefault(c => c.DepartmentID == (int)keyValues.Single());
        }
    }
}
