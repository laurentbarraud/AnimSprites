using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimSprites
{
    public partial class frmAnimSprites : Form
    {
        public frmAnimSprites()
        {
            InitializeComponent();
        }

        private void frmAnimSprites_Load(object sender, EventArgs e)
        {
            // Crée un objet représentant l'image que l'on veut afficher.
            // Cet objet doit être un membre de la classe qui hérite de Image,
            // comme un Bitmap ou Metafile (WMF).
            Bitmap bmp = new Bitmap(@"D:\doc\Cpnv\MA-24-appui-programmation\AnimSprites\AnimSprites\Resources\nature-tileset.jpg");

            // Crée un rectangle pour afficher l'image.
            Rectangle destRect = new Rectangle(100, 100, 100, 100);

            // Crée un rectangle pour la source de l'image.
            Rectangle srcRect = new Rectangle(50, 50, 50, 50);

            // Crée un objet graphique qui représente la surface de dessin de notre bitmap.   
            Graphics g = Graphics.FromImage(bmp);

            // Définit les unités utilisées pour les coordonnées du rectangle source.
            GraphicsUnit units = GraphicsUnit.Pixel;

            picPlateforme2.Image = bmp;

            // Appelle la fonction DrawImage de l'objet graphique pour rendre l'image à l'écran.
            // On doit spécifier à la fois l'image et les coordonnées où elle va être dessinée.
            g.DrawImage(picPlateforme2.Image, destRect, srcRect, units);

            // On doit ensuite disposer de l'objet graphique.
            g.Dispose();
           
        }
    }
}


