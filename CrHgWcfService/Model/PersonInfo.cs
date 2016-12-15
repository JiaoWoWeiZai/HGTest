using CrHgWcfService.Common;

namespace CrHgWcfService.Model
{
    public class PersonInfo
    {
        public PersonInfo(int interfaceId)
        {
            var count = HgEngine.getrowcount(interfaceId);
            HgEngine.setresultset(interfaceId, "PersonInfo");

            var pvalue = "";
            HgEngine.getbyname(interfaceId, "name", ref pvalue);
            name = pvalue;
            HgEngine.getbyname(interfaceId, "indi_id", ref pvalue);
            indi_id = pvalue;
            HgEngine.getbyname(interfaceId, "sex", ref pvalue);
            sex = pvalue;
            HgEngine.getbyname(interfaceId, "pers_identity", ref pvalue);
            pers_identity = pvalue;
            HgEngine.getbyname(interfaceId, "pers_status", ref pvalue);
            pers_status = pvalue;
            HgEngine.getbyname(interfaceId, "office_grade", ref pvalue);
            office_grade = pvalue;
            HgEngine.getbyname(interfaceId, "idcard", ref pvalue);
            idcard = pvalue;
            HgEngine.getbyname(interfaceId, "telephone", ref pvalue);
            telephone = pvalue;
            HgEngine.getbyname(interfaceId, "birthday", ref pvalue);
            birthday = pvalue;
            HgEngine.getbyname(interfaceId, "post_code", ref pvalue);
            post_code = pvalue;
            HgEngine.getbyname(interfaceId, "corp_id", ref pvalue);
            corp_id = pvalue;
            HgEngine.getbyname(interfaceId, "corp_name", ref pvalue);
            corp_name = pvalue;
            HgEngine.getbyname(interfaceId, "last_balance", ref pvalue);
            last_balance = pvalue;


            if (count != 1) return;
            HgEngine.setresultset(interfaceId, "freezeinfo");
            HgEngine.getbyname(interfaceId, "fund_id", ref pvalue);
            fund_id = pvalue;
            HgEngine.getbyname(interfaceId, "fund_name", ref pvalue);
            fund_name = pvalue;
            HgEngine.getbyname(interfaceId, "indi_freeze_status", ref pvalue);
            indi_freeze_status = pvalue;
            HgEngine.setresultset(interfaceId, "clinicapplyinfo");
            HgEngine.getbyname(interfaceId, "serial_apply", ref pvalue);
            serial_apply = pvalue;
        }
        public string indi_id { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string pers_identity { get; set; }
        public string pers_status { get; set; }
        public string office_grade { get; set; }
        public string idcard { get; set; }
        public string telephone { get; set; }
        public string birthday { get; set; }
        public string post_code { get; set; }
        public string corp_id { get; set; }
        public string corp_name { get; set; }
        public string last_balance { get; set; }
        public string fund_id { get; set; }
        public string fund_name { get; set; }
        public string indi_freeze_status { get; set; }
        public string serial_apply { get; set; }
    }
}