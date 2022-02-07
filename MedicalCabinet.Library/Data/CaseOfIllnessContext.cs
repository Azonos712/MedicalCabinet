using MedicalCabinet.Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCabinet.Library.Data
{
    public class CaseOfIllnessContext
    {
        public CaseOfIllness Case { get; private set; }

        public CaseOfIllnessContext(CaseOfIllness _case)
        {
            Case = _case;
        }

        public void AddCase()
        {
            using (var db = new ApplicationContext())
            {
                db.CasesOfIllnesses.Add(Case);
                db.SaveChanges();
            }
        }

        public void UpdateCase()
        {
            using (var db = new ApplicationContext())
            {
                db.CasesOfIllnesses.Update(Case);
                db.SaveChanges();
            }
        }

        public void DeleteCase()
        {
            using (var db = new ApplicationContext())
            {
                db.CasesOfIllnesses.Remove(Case);
                db.SaveChanges();
            }
        }

        public CaseOfIllness GetCaseById()
        {
            using (var db = new ApplicationContext())
            {
                return db.CasesOfIllnesses
                    .Include(c => c.Patient)
                    .Include(c => c.Doctor)
                .Single(c => c.Id == Case.Id);
            }
        }
    }
}
