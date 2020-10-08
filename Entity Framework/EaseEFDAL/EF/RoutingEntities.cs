namespace EaseEFDAL.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EaseEFDAL.Models;

    public partial class RoutingEntities : DbContext
    {
        public RoutingEntities()
            : base("name=EaseEFDALConnection")
        {
        }


        public virtual DbSet<PARTXREF> PARTXREFs { get; set; }

        public virtual DbSet<ROUTEHDR> ROUTEHDRs { get; set; }

        public virtual DbSet<OPHDR> OPHDRs { get; set; }

        public virtual DbSet<SUBHDR> SUBHDRs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OPHDR>()
                .Property(e => e.ID)
                .HasPrecision(10, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.RECTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.SEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.OPSEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.OPNO)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.OPDESC)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.WORKCENT)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.MAXBATCH)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.DATAREC)
                .HasPrecision(10, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.PFDCYC)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.PFDSETUP)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.REALCYC)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.REALSETUP)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.ACOSTRATE)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.ACYCTIME)
                .HasPrecision(12, 7);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.ASETUPTIME)
                .HasPrecision(12, 7);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.MATRECNO)
                .HasPrecision(10, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.MACHRECNO)
                .HasPrecision(10, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.EffectiveFrom)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.ISMETRIC)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.MISCFLAG1)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.PCNNO)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.MISCFLAG2)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.USERDEF1)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.USERDEF2)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.USERDEF3)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.TOOLCOSTRUN)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.TOOLCOSTSU)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.NSL)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.PROCESSFLAG)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.NOMEN)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.BALANCEFLAG)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.ALTFLAG)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.COSTKEY)
                .HasPrecision(10, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.RULEID)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.STATIONTIME)
                .HasPrecision(12, 7);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.Engineer)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.ReleasedFlag)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.InUseFlag)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.CRITOPTIME)
                .HasPrecision(12, 7);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.CRITOP)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.VA)
                .HasPrecision(9, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.NVA)
                .HasPrecision(9, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.ESSNVA)
                .HasPrecision(9, 5);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.OpRev)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.CheckOut_Engineer)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.OpRelDate)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.EffectiveTo)
                .HasPrecision(5, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.SPARE1)
                .IsUnicode(false);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.SHAREDOPID)
                .HasPrecision(10, 0);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.BasicRunTime)
                .HasPrecision(12, 7);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.BasicSetupTime)
                .HasPrecision(12, 7);

            modelBuilder.Entity<OPHDR>()
                .Property(e => e.KVICODE)
                .IsUnicode(false);

            modelBuilder.Entity<PARTXREF>()
                .Property(e => e.ID)
                .HasPrecision(10, 0);

            modelBuilder.Entity<PARTXREF>()
                .Property(e => e.PARTNO)
                .IsUnicode(false);

            modelBuilder.Entity<PARTXREF>()
                .Property(e => e.PlantID)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.ID)
                .HasPrecision(10, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.RECTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.SEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.LOTSIZE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.AUTHREL)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.ENG)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.RELDATE)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.USER0)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.USER1)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.USER2)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.USER3)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.MATCOST)
                .HasPrecision(12, 5);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.EXPUNIT)
                .HasPrecision(12, 5);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.EXPLOT)
                .HasPrecision(12, 5);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.CCOST)
                .HasPrecision(12, 5);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.SCOST)
                .HasPrecision(12, 5);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.CHANGE)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.TCOST)
                .HasPrecision(12, 5);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.TOOLCOSTS)
                .HasPrecision(12, 5);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.NOP)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.PARTDESC)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.NET)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.USERID)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.SETUPHRS)
                .HasPrecision(12, 7);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.RUNHRS)
                .HasPrecision(12, 7);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.FLAG1)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.FLAG2)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.FLAG3)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.MASTERPART)
                .IsUnicode(false);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.TAKTTIME)
                .HasPrecision(12, 7);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.NetDate)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.PLANTID)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.PARTREV)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.LOCKPLAN)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.ContinousFlow)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ROUTEHDR>()
                .Property(e => e.SHLevel)
                .HasPrecision(5, 0);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.ID)
                .HasPrecision(10, 0);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.RECTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.SEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.OPNO)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.SHID)
                .HasPrecision(10, 0);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.SHSEQ)
                .HasPrecision(5, 0);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.DESCX)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.CHANGEFLAG)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.MODEL1)
                .HasPrecision(12, 5);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.MODEL2)
                .HasPrecision(12, 5);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.DESTINATION1)
                .HasPrecision(12, 5);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.DESTINATION2)
                .HasPrecision(12, 5);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.OPTION1)
                .HasPrecision(12, 5);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.OPTION2)
                .HasPrecision(12, 5);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.EXCLUDEFROMPRINT)
                .HasPrecision(9, 5);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.SETUPTIME)
                .HasPrecision(12, 7);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.CYCLETIME)
                .HasPrecision(12, 7);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.GANTTSTARTFROM)
                .HasPrecision(5, 0);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.LBSTICKY)
                .HasPrecision(5, 0);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.MISCFLAG1)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.MISCFLAG2)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.MISCFLAG3)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.MISCFLAG4)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.SHType)
                .HasPrecision(5, 0);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.LaborType)
                .HasPrecision(5, 0);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.NumMen)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.DESC2)
                .IsUnicode(false);

            modelBuilder.Entity<SUBHDR>()
                .Property(e => e.PLATTENID)
                .IsUnicode(false);
        }
    }
}
