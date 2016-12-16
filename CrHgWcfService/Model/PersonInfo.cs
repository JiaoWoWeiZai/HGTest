using CrHgWcfService.Common;

namespace CrHgWcfService.Model
{
    public class PersonInfo
    {
        public PersonInfo(int interfaceId)
        {
            var count = HgEngine.GetRowCount(interfaceId);
            HgEngine.setresultset(interfaceId, "PersonInfo");

            var pvalue = "";
            HgEngine.GetByName(interfaceId, "name", ref pvalue);
            Name = pvalue;
            HgEngine.GetByName(interfaceId, "indi_id", ref pvalue);
            IndiId = pvalue;
            HgEngine.GetByName(interfaceId, "sex", ref pvalue);
            Sex = pvalue;
            HgEngine.GetByName(interfaceId, "pers_identity", ref pvalue);
            PersIdentity = pvalue;
            HgEngine.GetByName(interfaceId, "pers_status", ref pvalue);
            PersStatus = pvalue;
            HgEngine.GetByName(interfaceId, "office_grade", ref pvalue);
            OfficeGrade = pvalue;
            HgEngine.GetByName(interfaceId, "idcard", ref pvalue);
            Idcard = pvalue;
            HgEngine.GetByName(interfaceId, "telephone", ref pvalue);
            Telephone = pvalue;
            HgEngine.GetByName(interfaceId, "birthday", ref pvalue);
            Birthday = pvalue;
            HgEngine.GetByName(interfaceId, "post_code", ref pvalue);
            PostCode = pvalue;
            HgEngine.GetByName(interfaceId, "corp_id", ref pvalue);
            CorpId = pvalue;
            HgEngine.GetByName(interfaceId, "corp_name", ref pvalue);
            CorpName = pvalue;
            HgEngine.GetByName(interfaceId, "last_balance", ref pvalue);
            LastBalance = pvalue;


            if (count != 1) return;
            HgEngine.setresultset(interfaceId, "freezeinfo");
            HgEngine.GetByName(interfaceId, "fund_id", ref pvalue);
            FundId = pvalue;
            HgEngine.GetByName(interfaceId, "fund_name", ref pvalue);
            FundName = pvalue;
            HgEngine.GetByName(interfaceId, "indi_freeze_status", ref pvalue);
            IndiFreezeStatus = pvalue;
            HgEngine.setresultset(interfaceId, "clinicapplyinfo");
            HgEngine.GetByName(interfaceId, "serial_apply", ref pvalue);
            SerialApply = pvalue;
        }
        public string IndiId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string PersIdentity { get; set; }
        public string PersStatus { get; set; }
        public string OfficeGrade { get; set; }
        public string Idcard { get; set; }
        public string Telephone { get; set; }
        public string Birthday { get; set; }
        public string PostCode { get; set; }
        public string CorpId { get; set; }
        public string CorpName { get; set; }
        public string LastBalance { get; set; }
        public string FundId { get; set; }
        public string FundName { get; set; }
        public string IndiFreezeStatus { get; set; }
        public string SerialApply { get; set; }
    }
}