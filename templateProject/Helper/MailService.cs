using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

using templateProject.Model;
using System.IO;
using System.Net.Mime;

namespace templateProject.Helper
{
    public class MailService
    {
        public static ResultStatusModel doSendMail(string toEmail, string cc, string bcc,
                            string subject, string messages, List<string> fileAttachments = null)
        {
            bool result = false;
            ResultStatusModel output = new ResultStatusModel();
            output.issuccess = true;
            try
            {
                //clsConfig cfg = new clsConfig();

                // Revised by Robin Frans ( check settingan email apakah ON atau OFF ) 
                string EmailPower = Configs.Mail_Power;
                if (EmailPower.ToUpper().Trim() != "ON")
                {
                    //clsLog.writeToLog("CommonService", "Setting email is OFF. Failed sending email to " + toEmail);
                    output.issuccess = false;
                    return output;
                }




                string smtp = Configs.Mail_smtp;
                string port = Configs.Mail_port;
                string from = Configs.Mail_from;
                string fromDisplay = Configs.Mail_fromDisplayName;
                string cfgBcc = Configs.Mail_Bcc;

                //clsLog.writeToLog("CommonService", "smtp = " + smtp);
                //clsLog.writeToLog("CommonService", "port = " + port);
                //clsLog.writeToLog("CommonService", "from = " + from);
                //clsLog.writeToLog("CommonService", "Bcc = " + cfgBcc);

                //clsLog.WriteToTextFile("doSendMail", "Bo.doSendMail", "smtp = " + smtp);
                //clsLog.WriteToTextFile("doSendMail", "Bo.doSendMail", "port = " + port);
                //clsLog.WriteToTextFile("doSendMail", "Bo.doSendMail", "from = " + from);
                //clsLog.WriteToTextFile("doSendMail", "Bo.doSendMail", "Bcc = " + cfgBcc);

                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient(smtp, Convert.ToInt32(port));

                if (!string.IsNullOrEmpty(toEmail))
                {
                    string[] sToEmail = toEmail.Split(";".ToCharArray());
                    int length = sToEmail.Length;
                    for (int i = 0; i < length; i++)
                    {
                        try
                        {
                            mail.To.Add(sToEmail[i].ToString() + Configs.Mail_Dummy);
                        }
                        catch (Exception ex)
                        {
                            //throw ex;
                        }
                    }
                }
                else
                {
                    output.issuccess = false;
                    output.err_msg = "Tujuan email tidak ditemukan.";
                    return output;
                }

                

                if (!string.IsNullOrEmpty(cc))
                {
                    string[] ccL = cc.Split(";".ToCharArray());
                    int len = ccL.Length;
                    for (int j = 0; j < len; j++)
                    {
                        try
                        {
                            mail.CC.Add(ccL[j].ToString() + Configs.Mail_Dummy);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                if (!string.IsNullOrEmpty(cfgBcc))
                {
                    string[] sBcc = cfgBcc.Split(";".ToCharArray());
                    for (int k = 0; k < sBcc.Length; k++)
                    {
                        if (sBcc[k].ToString().Trim() != "")
                        {
                            try
                            {
                                mail.Bcc.Add(sBcc[k].ToString().Trim());
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(bcc))
                {
                    string[] sBcc = bcc.Split(";".ToCharArray());
                    for (int k = 0; k < sBcc.Length; k++)
                    {
                        if (sBcc[k].ToString().Trim() != "")
                        {
                            try
                            {
                                mail.Bcc.Add(sBcc[k].ToString().Trim() + Configs.Mail_Dummy);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }

                mail.From = new System.Net.Mail.MailAddress(from, fromDisplay);
                mail.Subject = subject;
                mail.Body = messages;
                mail.IsBodyHtml = true;

                if (fileAttachments != null)
                {
                    foreach (string item in fileAttachments)
                    {
                        Attachment data = new Attachment(item, MediaTypeNames.Application.Octet);
                        mail.Attachments.Add(data);
                    }
                }

                client.UseDefaultCredentials = false;
                client.EnableSsl = false;
                client.Send(mail);

                if (mail.Attachments != null)
                {
                    for (Int32 i = mail.Attachments.Count - 1; i >= 0; i--)
                    {
                        mail.Attachments[i].Dispose();
                    }
                    mail.Attachments.Clear();
                    mail.Attachments.Dispose();
                }
                mail.Dispose();
                mail = null;

                result = true;
                output.issuccess = true;
            }
            catch (Exception ex)
            {
                string innerEx = GeneralFunctions.GetExceptionMessage(ex);
                result = false;
                output.issuccess = false;
                output.err_msg = ex.Message + " (Inner Ex : " + innerEx + ")";
            }
            //return result;
            return output;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}