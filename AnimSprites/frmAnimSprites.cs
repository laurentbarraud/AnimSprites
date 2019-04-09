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
            Bitmap bmp = new Bitmap(AnimSprites.Properties.Resources.nature_tileset);

            // Crée un rectangle pour afficher l'image, avec comme paramètres la coordonnée x et y du coin supérieur-gauche, la largeur et la hauteur.
            Rectangle destRect = new Rectangle(0, 0, 50, 50);

            // Crée un rectangle pour la source de l'image (milieu de la plateforme).
            Rectangle srcRectPlateforme = new Rectangle(34, 2, 31, 31);

            // Crée un rectangle pour la source de l'image (bout gauche de la plateforme).
            Rectangle srcRectPlateformeGauche = new Rectangle(2, 2, 31, 31);

            // Crée un rectangle pour la source de l'image (bout droite de la plateforme).
            Rectangle srcRectPlateformeDroite = new Rectangle(67, 2, 31, 31);

            // Crée un objet graphique qui représente la surface de dessin de notre bitmap.   
            Graphics g = Graphics.FromImage(bmp);

            // Définit les unités utilisées pour les coordonnées du rectangle source.
            GraphicsUnit units = GraphicsUnit.Pixel;

            picPlateforme1.Image = bmp;
            picPlateforme2.Image = bmp;
            picPlateforme3.Image = bmp;
            picPlateforme4.Image = bmp;
            picPlateforme5.Image = bmp;
            picPlateforme6.Image = bmp;

            // Appelle la fonction DrawImage de l'objet graphique pour rendre les images à l'écran.
            // On doit spécifier à la fois l'image et les coordonnées où elle va être dessinée.
            g.DrawImage(picPlateforme1.Image, destRect, srcRectPlateformeGauche, units);
            g.DrawImage(picPlateforme2.Image, destRect, srcRectPlateforme, units);
            g.DrawImage(picPlateforme3.Image, destRect, srcRectPlateforme, units);
            g.DrawImage(picPlateforme4.Image, destRect, srcRectPlateforme, units);
            g.DrawImage(picPlateforme5.Image, destRect, srcRectPlateforme, units);
            g.DrawImage(picPlateforme6.Image, destRect, srcRectPlateformeDroite, units);

            // On doit ensuite disposer de l'objet graphique.
            g.Dispose();
           
        }
    }
}


