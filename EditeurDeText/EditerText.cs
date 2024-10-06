using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EditeurDeText
{
    public partial class EditerText : Form
    {

        private HashSet<int> lignesMarquees = new HashSet<int>();

        public EditerText()
        {
            InitializeComponent();
            label1.Text = "Nombre de lignes : 0";
            label2.Text = "Nombre de caractères : 0";
            MettreAJourNumerosLignes();

            // Initialisation de la barre de défilement
            vScrollBar1.Scroll += VScrollBar1_Scroll;
            this.Text = "EditerText";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MettreAJourNumerosLignes();

            // Compte le nombre de lignes dans la TextBox
            int lineCount = textBox1.Lines.Length;

            // Mets à jour le label avec le nombre de lignes
            label1.Text = "Nombre de lignes : " + lineCount.ToString();

            // Compte le nombre de caractères dans la TextBox
            int charCount = textBox1.Text.Length;

            // Mets à jour le label avec le nombre de caractères
            label2.Text = "Nombre de caractères : " + charCount.ToString();

            // Met à jour la barre de défilement
            vScrollBar1.Maximum = Math.Max(lineCount - 1, 0);
            vScrollBar1.Value = Math.Min(vScrollBar1.Value, vScrollBar1.Maximum);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditerText nouveauFormulaire = new EditerText();

            nouveauFormulaire.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Ouvre un fichier déjà existance sur le pc
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers texte (*.txt)|*.txt|Tous les fichiers (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = System.IO.File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Engresitre le texte sur le bureau
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fichiers texte (*.txt)|*.txt|Tous les fichiers (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, textBox1.Text);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void MettreAJourNumerosLignes()
        {
            // Efface le contenu de textBox2
            textBox2.Clear();

            // Récupère le nombre de lignes du textBox principal
            string[] lines = textBox1.Lines;

            // Parcours chaque ligne et l'ajoute à textBox2 avec son numéro
            for (int i = 0; i < lines.Length; i++)
            {
                // Vérifie si la ligne est marquée
                if (lignesMarquees.Contains(i))
                {
                    textBox2.AppendText($"* {i + 1}\n");
                }
                else
                {
                    textBox2.AppendText((i + 1).ToString() + Environment.NewLine);
                }
            }
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            // Défile le contenu de textBox1 en fonction de la valeur de la barre de défilement
            int nouvellePosition = vScrollBar1.Value;

            // Détermine le premier caractère visible dans textBox1
            int premierCaractereVisible = textBox1.GetFirstCharIndexFromLine(nouvellePosition);
            if (premierCaractereVisible >= 0)
            {
                textBox1.SelectionStart = premierCaractereVisible; // Positionne le curseur
                textBox1.ScrollToCaret(); // Fait défiler pour rendre le curseur visible
            }
        }

        private void buttonMarquer_Click(object sender, EventArgs e)
        {
            // Obtient la ligne sélectionnée
            int indexLigne = textBox1.GetLineFromCharIndex(textBox1.SelectionStart);
            if (indexLigne >= 0)
            {
                // Ajoute ou retire le marquage de la ligne
                if (lignesMarquees.Contains(indexLigne))
                {
                    lignesMarquees.Remove(indexLigne); // Retire le marquage
                }
                else
                {
                    lignesMarquees.Add(indexLigne); // Ajoute le marquage
                }

                // Met à jour les numéros de ligne pour refléter les marques
                MettreAJourNumerosLignes();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void EditerText_Load(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
