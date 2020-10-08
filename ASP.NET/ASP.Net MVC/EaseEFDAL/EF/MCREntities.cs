namespace EaseEFDAL.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EaseEFDAL.Models;

    public partial class MCREntities : DbContext
    {
        public MCREntities()
            : base("name=EaseEFDALConnection")
        {
        }

        public virtual DbSet<PCNAREATEMPLATE> PCNAREATEMPLATEs { get; set; }

        public virtual DbSet<PCNAUTHGROUP> PCNAUTHGROUPS { get; set; }

        public virtual DbSet<PCNCHANGETEXT> PCNCHANGETEXTs { get; set; }

        // Who uses PCNEnginesList - looks like GD

        public virtual DbSet<PCNHEADER> PCNHEADERs { get; set; }

        public virtual DbSet<PCNMM> PCNMMs { get; set; }

        public virtual DbSet<PCNNOTE> PCNNOTES { get; set; }

        // Warning Error 6013: The table/view 'EASE_DEMO.dbo.PCNNOTIFY' 
        // does not have a primary key defined and no valid primary key could be inferred. 
        // This table/view has been excluded. 
        // To use the entity, you will need to review your schema, add the correct keys, and uncomment it.

        public virtual DbSet<PCNNotifyQuestion> PCNNotifyQuestions { get; set; }

        public virtual DbSet<PCNPART> PCNPARTS { get; set; }

        // Warning Error 6013: The table/view 'EASE_DEMO.dbo.PCNQUESTION' 
        // does not have a primary key defined and no valid primary key could be inferred.
        // This table/view has been excluded.
        // To use the entity, you will need to review your schema, add the correct keys, and uncomment it.

        //Warning Error 6013: The table/view 'EASE_DEMO.dbo.PCNREJECT' 
        // does not have a primary key defined and no valid primary key could be inferred.
        // This table/view has been excluded.
        // To use the entity, you will need to review your schema, add the correct keys, and uncomment it.

        // Warning Error 6002: The table/view 'EASE_DEMO.dbo.PCNRFCCAT'
        // does not have a primary key defined.
        // The key has been inferred and the definition was created as a read-only table/view.

        public virtual DbSet<PCNRFCCAT> PCNRFCCATs { get; set; }

        public virtual DbSet<PCNRFCCATEGORY> PCNRFCCATEGORies { get; set; }

        public virtual DbSet<PCNRFCCOMMENT> PCNRFCCOMMENTs { get; set; }

        // Warning Error 6002: The table/view 'EASE_DEMO.dbo.PCNRFCGROUP' 
        // does not have a primary key defined.
        // The key has been inferred and the definition was created as a read-only table/view.

        public virtual DbSet<PCNRFCGROUP> PCNRFCGROUPs { get; set; }

        public virtual DbSet<PCNSD> PCNSDs { get; set; }

        //Warning Error 6002: The table/view 'EASE_DEMO.dbo.PCNSDAuth' 
        // does not have a primary key defined.
        // The key has been inferred and the definition was created as a read-only table/view.

        public virtual DbSet<PCNSDAuth> PCNSDAuths { get; set; }

        public virtual DbSet<PCNSDAUTHCNT> PCNSDAUTHCNTs { get; set; }

        public virtual DbSet<PCNTASKDueDateChanx> PCNTASKDueDateChanges { get; set; }

        public virtual DbSet<PCNTASKEscalate> PCNTASKEscalates { get; set; }

        public virtual DbSet<PCNTASKLIST> PCNTASKLISTs { get; set; }

        public virtual DbSet<PCNTASK> PCNTASKS { get; set; }

        //Warning Error 6002: The table/view 'EASE_DEMO.dbo.PCNWITEXT'
        // does not have a primary key defined.
        // The key has been inferred and the definition was created as a read-only table/view.

        public virtual DbSet<PCNWITEXT> PCNWITEXTs { get; set; }

        public virtual DbSet<PCNWITHDRAW> PCNWITHDRAWs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PCNAREATEMPLATE>()
                .Property(e => e.PLANTID)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNAREATEMPLATE>()
                .Property(e => e.TEMPLATEID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNAREATEMPLATE>()
                .Property(e => e.TEMPLATENAME)
                .IsUnicode(false);

            modelBuilder.Entity<PCNAUTHGROUP>()
                .Property(e => e.QID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNAUTHGROUP>()
                .Property(e => e.GROUPID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNAUTHGROUP>()
                .Property(e => e.NOTIFYFLAG)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNAUTHGROUP>()
                .Property(e => e.AUTHFLAG)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNCHANGETEXT>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNCHANGETEXT>()
                .Property(e => e.CHANGETYPE)
                .HasPrecision(3, 0);

            modelBuilder.Entity<PCNCHANGETEXT>()
                .Property(e => e.CHANGETEXT)
                .IsUnicode(false);

            modelBuilder.Entity<PCNCHANGETEXT>()
                .Property(e => e.CHANGETEXTSEQ)
                .HasPrecision(3, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.ECNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.PDNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.ENGINEER)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.PCNTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.PLANT)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.WORKGROUP)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.PCNCATEGORY)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.REQUESTDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.IMPLEMENTDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.EXPIRATIONDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.SIGNOFFDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.PCNSTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.COMMENTX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.REASON)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.NET)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.USERID)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.NETDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.MODEL)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.ALTPCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.PLANTID)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.ManagerOverRide)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.RFCCreatedDate)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.ProcDocWriter)
                .IsUnicode(false);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.CreatedDate)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.ActualImplementDate)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.PRIORITY)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.FastTrack)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNHEADER>()
                .Property(e => e.FTCategory)
                .IsUnicode(false);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.MSEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.TYPE)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.FILENAME)
                .IsUnicode(false);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.FILEPATHX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.WPTYPE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.TTKEY)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.DOCDESC)
                .IsUnicode(false);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.FILEDESCX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNMM>()
                .Property(e => e.VISUALAID)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNNOTE>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNNOTE>()
                .Property(e => e.ENTRYSEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNNOTE>()
                .Property(e => e.ENTRYDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNNOTE>()
                .Property(e => e.ENG)
                .IsUnicode(false);

            modelBuilder.Entity<PCNNOTE>()
                .Property(e => e.COMMENTSEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNNOTE>()
                .Property(e => e.COMMENTx)
                .IsUnicode(false);

            modelBuilder.Entity<PCNNotifyQuestion>()
                .Property(e => e.QID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNNotifyQuestion>()
                .Property(e => e.NOTIFICATIONQTN)
                .IsUnicode(false);

            modelBuilder.Entity<PCNNotifyQuestion>()
                .Property(e => e.PLANTID)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.PCNKEY)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.ID)
                .HasPrecision(10, 0);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.RECTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.SEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.PARTNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.OPNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.OPREV)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.CELLNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.APPROVEDSTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.PARTDESC)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.DBNAME)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.PLANTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.SHAREDPARTID)
                .HasPrecision(10, 0);

            modelBuilder.Entity<PCNPART>()
                .Property(e => e.SHAREDPARTNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNRFCCATEGORY>()
                .Property(e => e.CATEGORYID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCCATEGORY>()
                .Property(e => e.SEQ)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCCATEGORY>()
                .Property(e => e.CATEGORYDESC)
                .IsUnicode(false);

            modelBuilder.Entity<PCNRFCCOMMENT>()
                .Property(e => e.CATEGORYID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCCOMMENT>()
                .Property(e => e.COMMENTID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCCOMMENT>()
                .Property(e => e.SEQ)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCCOMMENT>()
                .Property(e => e.COMMENTDESC)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSD>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSD>()
                .Property(e => e.PCNKey)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSD>()
                .Property(e => e.DocID)
                .HasPrecision(12, 0);

            modelBuilder.Entity<PCNSD>()
                .Property(e => e.DocDesc)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSD>()
                .Property(e => e.RevNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSD>()
                .Property(e => e.DocSeq)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSD>()
                .Property(e => e.ApprovedStatus)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSD>()
                .Property(e => e.LocationDoc)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSD>()
                .Property(e => e.KeyFieldValue)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSDAUTHCNT>()
                .Property(e => e.DocID)
                .HasPrecision(12, 0);

            modelBuilder.Entity<PCNSDAUTHCNT>()
                .Property(e => e.DocSeq)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSDAUTHCNT>()
                .Property(e => e.NUMSIGN)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSDAUTHCNT>()
                .Property(e => e.DONESIGN)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSDAUTHCNT>()
                .Property(e => e.AUTHSTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSDAUTHCNT>()
                .Property(e => e.AWAITINGCHECKIN)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSDAUTHCNT>()
                .Property(e => e.PENDINGDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSDAUTHCNT>()
                .Property(e => e.LocationDoc)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.MSEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.TaskDESCX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.TaskSEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.DATEX)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.OldDATEX)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.NewDATEX)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.CommentX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.ChangedBy)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.Spare1)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.Spare2)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKDueDateChanx>()
                .Property(e => e.Spare3)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKEscalate>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKEscalate>()
                .Property(e => e.GROUPID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNTASKEscalate>()
                .Property(e => e.MSEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKEscalate>()
                .Property(e => e.TASKDESCX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKEscalate>()
                .Property(e => e.ManagerID)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKEscalate>()
                .Property(e => e.DUEDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKEscalate>()
                .Property(e => e.EscalateDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKEscalate>()
                .Property(e => e.REASONX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKLIST>()
                .Property(e => e.PLANTID)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKLIST>()
                .Property(e => e.TEMPLATEID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNTASKLIST>()
                .Property(e => e.GROUPID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNTASKLIST>()
                .Property(e => e.USERID)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKLIST>()
                .Property(e => e.UserRole)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKLIST>()
                .Property(e => e.Manager)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKLIST>()
                .Property(e => e.TaskDesc)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASKLIST>()
                .Property(e => e.Critical)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASKLIST>()
                .Property(e => e.TaskNum)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.TEMPLATEID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.GROUPID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.MSEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.DESCX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.APPROVERID)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.DUEDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.APPLICABLE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.CRITICAL)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.TASKSTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.SignoffDate)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.UserRole)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.Manager)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.EscalateDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.TaskSeq)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.COMMENTX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNTASK>()
                .Property(e => e.SEVERITY)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNWITHDRAW>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNWITHDRAW>()
                .Property(e => e.WITHDRAWTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<PCNWITHDRAW>()
                .Property(e => e.REASON)
                .IsUnicode(false);

            modelBuilder.Entity<PCNRFCCAT>()
                .Property(e => e.GROUPID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCCAT>()
                .Property(e => e.CATEGORYID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCCAT>()
                .Property(e => e.SEQ_)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCCAT>()
                .Property(e => e.CATEGORYNAME)
                .IsUnicode(false);

            modelBuilder.Entity<PCNRFCGROUP>()
                .Property(e => e.GROUPID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCGROUP>()
                .Property(e => e.SEQ_)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNRFCGROUP>()
                .Property(e => e.GROUPNAME)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSDAuth>()
                .Property(e => e.DocID)
                .HasPrecision(12, 0);

            modelBuilder.Entity<PCNSDAuth>()
                .Property(e => e.DocSeq)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSDAuth>()
                .Property(e => e.GROUPID)
                .HasPrecision(9, 0);

            modelBuilder.Entity<PCNSDAuth>()
                .Property(e => e.AUTHORIZER)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSDAuth>()
                .Property(e => e.AUTHDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNSDAuth>()
                .Property(e => e.AUTHSTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSDAuth>()
                .Property(e => e.COMMENTX)
                .IsUnicode(false);

            modelBuilder.Entity<PCNSDAuth>()
                .Property(e => e.LocationDoc)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNWITEXT>()
                .Property(e => e.PCNNO)
                .IsUnicode(false);

            modelBuilder.Entity<PCNWITEXT>()
                .Property(e => e.TTKEY)
                .HasPrecision(5, 0);

            modelBuilder.Entity<PCNWITEXT>()
                .Property(e => e.WP)
                .HasPrecision(5, 0);
        }
    }
}
