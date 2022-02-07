using MedicalCabinet.Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCabinet.Library.Data
{
    public class NoteContext
    {
        public Note Note { get; private set; }

        public NoteContext(Note note)
        {
            Note = note;
        }

        public void AddNote()
        {
            using (var db = new ApplicationContext())
            {
                db.Notes.Add(Note);
                db.SaveChanges();
            }
        }

        public Note GetNoteById()
        {
            using (var db = new ApplicationContext())
            {
                return db.Notes
                    .Include(n => n.Doctor)
                        .ThenInclude(d => d.Notes)
                .Single(n => n.Id == Note.Id);
            }
        }

        public void DeleteNote()
        {
            using (var db = new ApplicationContext())
            {
                db.Notes.Remove(Note);
                db.SaveChanges();
            }
        }
    }
}
