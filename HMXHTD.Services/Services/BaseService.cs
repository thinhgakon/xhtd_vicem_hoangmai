using HMXHTD.Data.DataEntity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace HMXHTD.Services.Services
{
    public interface IBaseService<T> where T : class
    {
        int GetCount();
        IEnumerable<T> GetAll();
        T GetById(object id);
        T GetFirstOrDefault();
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        StaticPagedList<T> GetPagiationExt(IEnumerable<T> items, int page, int pageSize, int totalRow);

        StaticPagedList<T> GetPagiationBase(int page, int pageSize, string sort, string sortField);

        string FormatSQLInput(string Input);
        void SendMail(string FromName, string ToEmail, string ToName, string Subject, string Body);

        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);

    }
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Database _context = null;
        private DbSet<T> table = null;
        public BaseService()
        {
            this._context = new Database();
            table = _context.Set<T>();
        }
        public BaseService(Database _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public int GetCount()
        {
            return table.Count();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public T GetFirstOrDefault()
        {
            return table.FirstOrDefault();
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            _context.SaveChanges();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public StaticPagedList<T> GetPagiationExt(IEnumerable<T> items, int page, int pageSize, int totalRow)
        {
            return new StaticPagedList<T>
                (
                    items, page, pageSize, totalRow
                    );
        }
        public StaticPagedList<T> GetPagiationBase(int page, int pageSize, string sort = "ASC", string sortField = "Id")
        {
            var totalRow = table.Count();
            var skip = (page - 1) * pageSize;
            var dynamicPropFromStr = typeof(T).GetProperty(sortField);
            var items = sort.Equals("DESC") ? table.AsEnumerable().OrderByDescending(x => dynamicPropFromStr.GetValue(x, null)).Skip(page).Take(pageSize).ToList() :
                table.AsEnumerable().OrderBy(x => dynamicPropFromStr.GetValue(x, null)).Skip(page).Take(pageSize).ToList();
            return new StaticPagedList<T>
                (
                    items, page, pageSize, totalRow
                    );
        }


        public string FormatSQLInput(string Input)
        {
            string SQLInput = string.Empty;
            SQLInput = Input;
            SQLInput = SQLInput.Replace("<", "&lt;");
            SQLInput = SQLInput.Replace(">", "&gt;");
            SQLInput = SQLInput.Replace('"'.ToString(), "");
            SQLInput = SQLInput.Replace("'", "''");
            SQLInput = SQLInput.Replace(";", "");
            SQLInput = SQLInput.Replace(":", "");
            SQLInput = SQLInput.Replace("/", "");
            SQLInput = SQLInput.Replace("%", "");
            SQLInput = SQLInput.Replace("$", "");
            SQLInput = SQLInput.Replace("^", "");
            SQLInput = SQLInput.Replace("*", "");
            SQLInput = SQLInput.Replace("or", "");
            SQLInput = SQLInput.Replace("select", "sel&#101;ct");
            SQLInput = SQLInput.Replace("join", "jo&#105;n");
            SQLInput = SQLInput.Replace("union", "un&#105;on");
            SQLInput = SQLInput.Replace("where", "wh&#101;re");
            SQLInput = SQLInput.Replace("insert", "ins&#101;rt");
            SQLInput = SQLInput.Replace("delete", "del&#101;te");
            SQLInput = SQLInput.Replace("update", "up&#100;ate");
            SQLInput = SQLInput.Replace("like", "lik&#101;");
            SQLInput = SQLInput.Replace("drop", "dro&#112;");
            SQLInput = SQLInput.Replace("create", "cr&#101;ate");
            SQLInput = SQLInput.Replace("modify", "mod&#105;fy");
            SQLInput = SQLInput.Replace("rename", "ren&#097;me");
            SQLInput = SQLInput.Replace("alter", "alt&#101;r");
            SQLInput = SQLInput.Replace("cast", "ca&#115;t");
            SQLInput = SQLInput.Replace("OR", "");
            SQLInput = SQLInput.Replace("SELECT", "sel&#101;ct");
            SQLInput = SQLInput.Replace("JOIN", "jo&#105;n");
            SQLInput = SQLInput.Replace("UNION", "un&#105;on");
            SQLInput = SQLInput.Replace("WHERE", "wh&#101;re");
            SQLInput = SQLInput.Replace("INSERT", "ins&#101;rt");
            SQLInput = SQLInput.Replace("DELETE", "del&#101;te");
            SQLInput = SQLInput.Replace("UPDATE", "up&#100;ate");
            SQLInput = SQLInput.Replace("LIKE", "lik&#101;");
            SQLInput = SQLInput.Replace("DROP", "dro&#112;");
            SQLInput = SQLInput.Replace("CREATE", "cr&#101;ate");
            SQLInput = SQLInput.Replace("MODIFY", "mod&#105;fy");
            SQLInput = SQLInput.Replace("RENAME", "ren&#097;me");
            SQLInput = SQLInput.Replace("ALTER", "alt&#101;r");
            SQLInput = SQLInput.Replace("CAST", "ca&#115;t");
            return SQLInput;
        }


        public string FormatToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public void SendMail(string FromName, string ToEmail, string ToName, string Subject, string Body)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                //var setting = db.Settings.FirstOrDefault(a => a.Setting_ID == 1);

                //SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Host = setting.EmailSenderSMTP;
                //smtpClient.Port = int.Parse(setting.EmailSenderPort);
                //smtpClient.EnableSsl = setting.EmailSenderSSL.Value;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtpClient.Credentials = new NetworkCredential(setting.EmailSender, setting.EmailSenderPassword);
                //smtpClient.Timeout = 20000;

                //MailAddress FromAddress = new MailAddress(setting.EmailSender, setting.AdminName);
                //MailAddress ToAddress = new MailAddress(ToEmail, ToName);
                //MailMessage Mailer = new MailMessage(FromAddress, ToAddress);
                //Mailer.IsBodyHtml = true;
                //Mailer.BodyEncoding = System.Text.Encoding.UTF8;
                //Mailer.Subject = Subject;
                //Mailer.Body = Body;
                //smtpClient.Send(Mailer);
            }

        }


        public void LogInfo(string message)
        {
            try
            {
                log.Info(message);
            }
            catch (Exception)
            {

            }
        }
        public void LogWarning(string message)
        {
            try
            {
                log.Warn(message);
            }
            catch (Exception)
            {

            }
        }
        public void LogError(string message)
        {
            try
            {
                log.Error(message);
            }
            catch (Exception)
            {

            }
        }

    }
}
