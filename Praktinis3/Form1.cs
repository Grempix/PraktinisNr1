using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktinis3
{
    public partial class Form1 : Form
    {

        // Nustatyti kintamųjų get ir set. Pagrindiniai kintamieji pakeisti į private tipą.

        private int vaiku_skaicius;

        public int Vaiku_skaicius
        {
            get { return vaiku_skaicius; }
            set { vaiku_skaicius = value; }
        }
        private double vaiko_islaikimas;

        public double Vaiko_islaikimas
        {
            get { return vaiko_islaikimas; }
            set { vaiko_islaikimas = value; }
        }
        private double seimos_pajamos;

        public double Seimos_pajamos
        {
            get { return seimos_pajamos; }
            set { seimos_pajamos = value; }
        }
        private double seimos_skolos;

        public double Seimos_skolos
        {
            get { return seimos_skolos; }
            set { seimos_skolos = value; }
        }
        private double paskolos_suma;

        public double Paskolos_suma
        {
            get { return paskolos_suma; }
            set { paskolos_suma = value; }
        }
        private int pastato_amzius;

        public int Pastato_amzius
        {
            get { return pastato_amzius; }
            set { pastato_amzius = value; }
        }
        private double paskolos_palukanos;

        public double Paskolos_palukanos
        {
            get { return paskolos_palukanos; }
            set { paskolos_palukanos = value; }
        }
        private double tikros_pajamos;

        public double Tikros_pajamos
        {
            get { return tikros_pajamos; }
            set { tikros_pajamos = value; }
        }
        private int grazinimo_laikotarpis;

        public int Grazinimo_laikotarpis
        {
            get { return grazinimo_laikotarpis; }
            set { grazinimo_laikotarpis = value; }
        }
        private double sutarties_pasirasimo_mokestis;

        public double Sutarties_pasirasimo_mokestis
        {
            get { return sutarties_pasirasimo_mokestis; }
            set { sutarties_pasirasimo_mokestis = value; }
        }

        private double viso_grazinti = 0;

        public double Viso_grazinti
        {
            get { return viso_grazinti; }
            set { viso_grazinti = value; }
        }


        public Form1()
        {
            InitializeComponent();
            bPaskola.Hide();
            vKreditas.Hide();
            groupBox1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int arSkaiciuoti = this.pradiniuDuomenuTikrinimas(1);
                if (arSkaiciuoti == 0)
                {

                    label19.Text = Math.Round(this.Paskolos_suma, 2) +" €";
                    label21.Text = Math.Round(this.Viso_grazinti, 2) + " €";
                    label23.Text = Math.Round(this.menesineImoka(), 2) + " €";

                    // Rezultata pateikti
                    groupBox1.Show();
                    this.Viso_grazinti = 0;
                }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int arSkaiciuoti = this.pradiniuDuomenuTikrinimas(2);
                if (arSkaiciuoti == 0)
                {


                    label19.Text = Math.Round(this.Paskolos_suma, 2) + " €";
                    label21.Text = Math.Round(this.Viso_grazinti, 2) + " €";
                    label23.Text = Math.Round(this.menesineImoka(), 2) + " €";

                    // Rezultata pateikti
                    groupBox1.Show();
                    this.Viso_grazinti = 0;
                }
        }
        private double menesineImoka()
        {
            return (this.Viso_grazinti / (this.Grazinimo_laikotarpis * 12));
        }
        private void grazinimoLaikotarpis(int paskolosTipas)
        {
            if (paskolosTipas == 1)
            {
                this.Grazinimo_laikotarpis = ((50 - this.Pastato_amzius) > 40) ? 40 : (50 - this.Pastato_amzius);

                label18.Text = this.Grazinimo_laikotarpis + " m.";
            }
            if (paskolosTipas == 2)
            {
                this.Grazinimo_laikotarpis = 5;

                label18.Text = this.Grazinimo_laikotarpis + " m.";
            }
        }
        private void liekaPajamu()
        {
            this.Tikros_pajamos = this.Seimos_pajamos - (this.Vaiku_skaicius * this.Vaiko_islaikimas) - this.Seimos_skolos;
            label25.Text = "" + this.Tikros_pajamos;
        }
        private void metinesPalukanosSuma()
        {
            this.Viso_grazinti = this.Sutarties_pasirasimo_mokestis + this.Paskolos_suma + ((this.Paskolos_suma / 100 * this.Paskolos_palukanos)/* * this.Grazinimo_laikotarpis*/);
        }


        // salyginių sakinių tikrinimo supaprastinimas
        private bool TikrinimasOr(int skaicius1, int skaicius2, double lyginimas)
        {
            return lyginimas < skaicius1 || lyginimas > skaicius2;
        }

        private bool TikrinimasAnd1(int skaicius1, int skaicius2, double lyginimas)
        {
            return lyginimas <= skaicius1 && lyginimas > skaicius2;
        }

        private bool TikrinimasAnd2(int skaicius1, int skaicius2, double lyginimas)
        {
            return lyginimas >= skaicius1 && lyginimas == skaicius2;
        }

        private bool TikrinimasPaskolosSuma(int skaicius1, double lyginimas)
        {
            return lyginimas < skaicius1;
        }

        private bool TikrinimasPastatoAmzius(int skaicius1, double lyginimas)
        {
            return lyginimas > skaicius1;
        }

        private bool TikrinimasPaskolosSuteikimas(double skaicius1, double lyginimas1)
        {
            return ((lyginimas1 * skaicius1) < this.menesineImoka());
        }

        private int pradiniuDuomenuTikrinimas(int paskolosTipas)
        {


            // BŪSTO PASKOLA
            if (paskolosTipas == 1)
            {
                if (this.textBox1.Text == "") this.textBox1.Text = "0";
                if (this.textBox2.Text == "") this.textBox2.Text = "0";
                if (this.textBox3.Text == "") this.textBox3.Text = "0";
                if (this.textBox4.Text == "") this.textBox4.Text = "0";
                if (this.textBox5.Text == "") this.textBox5.Text = "0";
                if (this.textBox11.Text == "") this.textBox11.Text = "0";
                if (this.textBox12.Text == "") this.textBox12.Text = "0";


                //pradiniai duomenys
                this.Vaiku_skaicius = Convert.ToInt32(this.textBox1.Text);
                this.Vaiko_islaikimas = Convert.ToDouble(this.textBox5.Text);
                this.Seimos_pajamos = Convert.ToDouble(this.textBox2.Text);
                this.Seimos_skolos = Convert.ToDouble(this.textBox3.Text);
                this.Paskolos_suma = Convert.ToDouble(this.textBox4.Text);
                this.Pastato_amzius = Convert.ToInt32(this.textBox11.Text);
                this.Paskolos_palukanos = Convert.ToDouble(this.textBox12.Text);

                this.liekaPajamu();
                this.grazinimoLaikotarpis(paskolosTipas);
                this.sutartiesSudarymoMokestis(paskolosTipas);
                this.metinesPalukanosSuma();

                // Vaikų skaičius
                if (TikrinimasOr(0, 10, this.Vaiku_skaicius))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Vaikų skaičius gali būti nuo 0 iki 10!");
                    return 1;
                }
                else if (TikrinimasAnd1(0, 1, this.Vaiko_islaikimas))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Vaiko išlaikimo išlaidos turi viršyti 0");
                    return 1;
                }
                else if (TikrinimasAnd2(1, 0, this.Vaiko_islaikimas))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Jūs negalite skirti vaikų išlaikimui, nes jų neturite!");
                    return 1;
                }
                else if (TikrinimasOr(0, 30000, this.Seimos_pajamos))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Šeimos pajamos turi sudaryti nuo 0 iki 30000 €");
                    return 1;
                }
                else if (TikrinimasOr(0, 15000, this.Seimos_skolos))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Šeimos skola turi sudaryti nuo 0 iki 15000 €");
                    return 1;
                }
                else if (TikrinimasPaskolosSuma(0, this.Paskolos_suma))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskolos suma turi būti daugiau nei 0!");
                    return 1;
                }
                else if (TikrinimasOr(0, 100, this.Paskolos_palukanos))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskolos palūkanos turi sudaryti nuo 0 iki 100%!");
                    return 1;
                }
                else if (TikrinimasPastatoAmzius(40, this.Pastato_amzius))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Pastato amžius ne daugiau nei 40!");
                    return 1;
                }
                else if (TikrinimasPaskolosSuteikimas(0.4, this.Tikros_pajamos))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskola negali būti suteikta, nes jūs esate per daug išsiskolines!");
                    return 1;
                }
            }
            // VARTOJIMO KREDITAS
            if (paskolosTipas == 2)
            {
                if (this.textBox7.Text == "") this.textBox7.Text = "0";
                if (this.textBox6.Text == "") this.textBox6.Text = "0";
                if (this.textBox8.Text == "") this.textBox8.Text = "0";
                if (this.textBox9.Text == "") this.textBox9.Text = "0";
                if (this.textBox10.Text == "") this.textBox10.Text = "0";
                if (this.textBox13.Text == "") this.textBox13.Text = "0";

                //pradiniai duomenys
                this.Vaiku_skaicius = Convert.ToInt32(this.textBox7.Text);
                this.Vaiko_islaikimas = Convert.ToDouble(this.textBox6.Text);
                this.Seimos_pajamos = Convert.ToDouble(this.textBox8.Text);
                this.Seimos_skolos = Convert.ToDouble(this.textBox9.Text);
                this.Paskolos_suma = Convert.ToDouble(this.textBox10.Text);
                this.Paskolos_palukanos = Convert.ToDouble(this.textBox13.Text);

                this.liekaPajamu();
                this.grazinimoLaikotarpis(paskolosTipas);
                this.sutartiesSudarymoMokestis(paskolosTipas);
                this.metinesPalukanosSuma();

                // Vaikų skaičius
                if (TikrinimasOr(0, 10, this.Vaiku_skaicius))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Vaikų skaičius gali būti nuo 0 iki 10!");
                    return 1;
                }
                else if (TikrinimasAnd1(0, 1, this.Vaiku_skaicius))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Vaiko išlaikimo išlaidos turi viršyti 0");
                    return 1;
                }
                else if (TikrinimasAnd2(1, 0, this.Vaiko_islaikimas))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Jūs negalite skirti vaikų išlaikimui, nes jų neturite!");
                    return 1;
                }
                else if (TikrinimasOr(0, 10000, this.Seimos_pajamos))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Šeimos pajamos turi sudaryti nuo 0 iki 30000 €");
                    return 1;
                }
                else if (TikrinimasOr(0, 5000, this.Seimos_skolos))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Šeimos skola turi sudaryti nuo 0 iki 15000 €");
                    return 1;
                }
                else if (TikrinimasOr(300, 26000, this.Paskolos_suma))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskolos suma turi būti nuo 300 iki 26000 €!");
                    return 1;
                }
                else if (TikrinimasOr(0, 100, this.Paskolos_palukanos))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskolos palūkanos turi sudaryti nuo 0 iki 100%!");
                    return 1;
                }
                else if (TikrinimasPaskolosSuteikimas(0.4, this.Tikros_pajamos))
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskola negali būti suteikta, nes jūs esate per daug išsiskolines!");
                    return 1;
                }
            }
            return 0;
        }

        private double sutartiesSudarymoMokestis(int paskolosTipas)
        {
            double mokestis;
            if (paskolosTipas == 1)
            {
                mokestis = this.Paskolos_suma * 0.13;
                if (mokestis < 144.81)
                {
                    label22.Text = 144.81 + " €";
                    this.Sutarties_pasirasimo_mokestis = 144.81;
                    return 144.81;
                }
                else
                {
                    label22.Text = mokestis + " €";
                    this.Sutarties_pasirasimo_mokestis = mokestis;
                    return mokestis;
                }
            }
            if (paskolosTipas == 2)
            {
                mokestis = this.Paskolos_suma / 100;
                if (mokestis < 28.96)
                {
                    label22.Text = 28.96 + " €";
                    this.Sutarties_pasirasimo_mokestis = 28.96;
                    return 28.96;
                }
                else
                {
                    label22.Text = mokestis + " €";
                    this.Sutarties_pasirasimo_mokestis = mokestis;
                    return mokestis;
                }
            }
            return 0;
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void _CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bPaskola_Click(object sender, EventArgs e)
        {

        }

        private void vKreditas_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }
    }
}
