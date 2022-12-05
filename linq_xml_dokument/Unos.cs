using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace linq_xml_dokument
{
    public partial class Unos : Form
    {
        List<Osoba> osobe=new List<Osoba>();
        string dokument = "osobe.xml";
        public Unos()
        {
            InitializeComponent();
        }

        private void btnUnosPodataka_Click(object sender, EventArgs e)
        {

            Osoba osoba = new Osoba(txtIme.Text, txtPrezime.Text, Convert.ToInt32(txtGodina.Text));

            osobe.Add(osoba);

            DialogResult upit =MessageBox.Show("Želite li upisati još osoba", "Upit", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);


            if (upit == DialogResult.Yes)
            {
               
                txtIme.Clear();
                txtPrezime.Clear();
                txtGodina.Clear();
                
            }
            else
            {
                if (File.Exists(dokument))
                {
                    var OsobeXml =XDocument.Load(dokument);
                    
                    foreach (Osoba os in osobe)
                    {
                        var Osoba = new XElement("Osoba",
                            new XElement("Ime", osoba.Ime),
                            new XElement("Prezime", osoba.Prezime),
                            new XElement("GodRod", osoba.Godina));
                        OsobeXml.Root.Add(Osoba);
                    }
                   
                    OsobeXml.Save(dokument);
                }
                else
                {
                    var OsobeXml = new XDocument();
                    OsobeXml.Add(new XElement("Osobe"));

                    foreach (Osoba os in osobe)
                    {
                        var Osoba = new XElement("Osoba",
                            new XElement("Ime", osoba.Ime),
                            new XElement("Prezime", osoba.Prezime),
                            new XElement("GodRod", osoba.Godina));
                        OsobeXml.Root.Add(Osoba);
                    }
                    OsobeXml.Save(dokument);

                }

                this.Close();

                //Spremanje dokumenta
                /*if (File.Exists(dokument))
                {
                    DialogResult overwrite = MessageBox.Show("Dokument već postoji", "Želite li prepisati stari dokument", 
                        MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);
                    if (overwrite == DialogResult.Yes)
                    {
                        OsobeXml.Save(dokument);
                    }
                    else
                    {
                        SaveFileDialog noviDokument=new SaveFileDialog();
                        noviDokument.InitialDirectory = @"C:\";
                        noviDokument.Title = "Spremi novi dokument";
                        noviDokument.DefaultExt = "xml";
                        noviDokument.ShowDialog();

                        dokument = noviDokument.FileName;
                        
                        OsobeXml.Save(dokument);
                    }

                }*/

            }
        }
    }
}
