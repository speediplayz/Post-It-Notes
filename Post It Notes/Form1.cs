using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Post_It_Notes
{
    public partial class Form1 : Form
    {
        List<Note> notes = new List<Note>();

        public Form1()
        {
            InitializeComponent();
            openFile();
        }

        public void openFile()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\notes.txt"))
            {
                File.Create("notes.txt");
                return;
            }

            StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\notes.txt");

            string[] file = reader.ReadToEnd().Split('|');

            foreach(string s in file){
                if(s != "")
                {
                    string[] split = s.Split('~');
                    Note note = new Note();
                    note.title = split[0].Split(':')[1];
                    note.desc = split[1].Split(':')[1];
                    notes.Add(note);
                }
            }

            reader.Close();
            reader.Dispose();

            updateList();
        }

        public void updateList()
        {
            txtNotes.Text = "";

            for(int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                txtNotes.AppendText(("ID: " + i.ToString() + " ----------" + Environment.NewLine));
                txtNotes.AppendText(("Name: " + note.title + Environment.NewLine));
                txtNotes.AppendText(("Desc: " + note.desc + Environment.NewLine));
                txtNotes.AppendText("" + Environment.NewLine);
                txtNotes.AppendText("" + Environment.NewLine);
                txtNotes.AppendText("" + Environment.NewLine);
            }
        }

        public void saveFile()
        {
            StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\notes.txt");

            foreach(Note note in notes){
                writer.Write(("title:" + note.title + "~desc:" + note.desc + "|"));
            }

            writer.Close();
            writer.Dispose();
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(txtNewTitle.Text != "" && txtNewDesc.Text != "")
            {
                Note note = new Note();
                note.title = txtNewTitle.Text;
                note.desc = txtNewDesc.Text;
                notes.Add(note);
            }

            saveFile();
            updateList();
        }

        private void deleteNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Note ID", "Delete Note");
            int id;
            if(int.TryParse(input, out id)){
                notes.RemoveAt(id);
                saveFile();
                updateList();
            }
        }
    }

    struct Note
    {
        public string title;
        public string desc;
    }
}
