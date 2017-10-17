using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Cookbook.DataModel;
using Cookbook.DataAccess;
using System.IO;

namespace Cookbook
{
    public partial class MainForm : Form
    {
        string dbName = "Cookbook.db3";
        string dbPath = Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)) + @"\";
        IDataRepository db;
        public MainForm()
        {
            InitializeComponent();

            IDataRepository db = new DataRepository(dbPath + dbName);
        }


    }
}