using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templateProject.Model
{
    public class ResultStatusModel
    {
        private string _msg { get; set; }
        private string _err_msg { get; set; }

        public ResultStatusModel() { }

        public ResultStatusModel(bool issuccess)
        {
            this.issuccess = issuccess;
        }

        public bool issuccess { get; set; }

        public string msg
        {
            get
            {
                if (string.IsNullOrEmpty(_msg)) { return string.Empty; }
                else { return _msg; }
            }
            set
            {
                if (string.IsNullOrEmpty(value)) { _msg = string.Empty; }
                else { _msg = value; }
            }
        }

        public string err_msg
        {
            get
            {
                if (string.IsNullOrEmpty(_err_msg)) { return string.Empty; }
                else { return _err_msg; }
            }
            set
            {
                if (string.IsNullOrEmpty(value)) { _err_msg = string.Empty; }
                else { _err_msg = value; }
            }
        }
    }
}
