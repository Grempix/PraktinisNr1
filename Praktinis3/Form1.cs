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

        int vaiku_skaicius;
        double vaiko_islaikimas;
        double seimos_pajamos;
        double seimos_skolos;
        double paskolos_suma;
        int pastato_amzius;
        double paskolos_palukanos;
        double tikros_pajamos;
        int grazinimo_laikotarpis;
        double sutarties_pasirasimo_mokestis;

        double viso_grazinti=0;


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

                    label19.Text = Math.Round(this.paskolos_suma, 2) +" €";
                    label21.Text = Math.Round(this.viso_grazinti, 2) + " €";
                    label23.Text = Math.Round(this.menesineImoka(), 2) + " €";

                    // Rezultata pateikti
                    groupBox1.Show();
                    this.viso_grazinti = 0;
                }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int arSkaiciuoti = this.pradiniuDuomenuTikrinimas(2);
                if (arSkaiciuoti == 0)
                {


                    label19.Text = Math.Round(this.paskolos_suma, 2) + " €";
                    label21.Text = Math.Round(this.viso_grazinti, 2) + " €";
                    label23.Text = Math.Round(this.menesineImoka(), 2) + " €";

                    // Rezultata pateikti
                    groupBox1.Show();
                    this.viso_grazinti = 0;
                }
        }
        private double menesineImoka()
        {
            return (this.viso_grazinti / (this.grazinimo_laikotarpis * 12));
        }
        private void grazinimoLaikotarpis(int paskolosTipas)
        {
            if (paskolosTipas == 1)
            {
                this.grazinimo_laikotarpis = ((50 - this.pastato_amzius) > 40) ? 40 : (50 - this.pastato_amzius);

                label18.Text = this.grazinimo_laikotarpis + " m.";
            }
            if (paskolosTipas == 2)
            {
                this.grazinimo_laikotarpis = 5;

                label18.Text = this.grazinimo_laikotarpis + " m.";
            }
        }
        private void liekaPajamu()
        {
            this.tikros_pajamos = this.seimos_pajamos - (this.vaiku_skaicius * this.vaiko_islaikimas) - this.seimos_skolos;
            label25.Text = "" + this.tikros_pajamos;
        }
        private void metinesPalukanosSuma()
        {
            this.viso_grazinti = this.sutarties_pasirasimo_mokestis + this.paskolos_suma + ((this.paskolos_suma / 100 * this.paskolos_palukanos)/* * this.grazinimo_laikotarpis*/);
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
                this.vaiku_skaicius = Convert.ToInt32(this.textBox1.Text);
                this.vaiko_islaikimas = Convert.ToDouble(this.textBox5.Text);
                this.seimos_pajamos = Convert.ToDouble(this.textBox2.Text);
                this.seimos_skolos = Convert.ToDouble(this.textBox3.Text);
                this.paskolos_suma = Convert.ToDouble(this.textBox4.Text);
                this.pastato_amzius = Convert.ToInt32(this.textBox11.Text);
                this.paskolos_palukanos = Convert.ToDouble(this.textBox12.Text);

                this.liekaPajamu();
                this.grazinimoLaikotarpis(paskolosTipas);
                this.sutartiesSudarymoMokestis(paskolosTipas);
                this.metinesPalukanosSuma();

                // Vaikų skaičius
                if (this.vaiku_skaicius < 0 || this.vaiku_skaicius > 10)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Vaikų skaičius gali būti nuo 0 iki 10!");
                    return 1;
                }
                else if (this.vaiko_islaikimas <= 0 && this.vaiku_skaicius > 1)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Vaiko išlaikimo išlaidos turi viršyti 0");
                    return 1;
                }
                else if (this.vaiko_islaikimas >= 1 && this.vaiku_skaicius == 0)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Jūs negalite skirti vaikų išlaikimui, nes jų neturite!");
                    return 1;
                }
                else if (this.seimos_pajamos < 0 || this.seimos_pajamos > 30000)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Šeimos pajamos turi sudaryti nuo 0 iki 30000 €");
                    return 1;
                }
                else if (this.seimos_skolos < 0 || this.seimos_skolos > 15000)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Šeimos skola turi sudaryti nuo 0 iki 15000 €");
                    return 1;
                }
                else if (this.paskolos_suma < 1)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskolos suma turi būti daugiau nei 0!");
                    return 1;
                }
                else if (this.paskolos_palukanos < 0 || this.paskolos_palukanos > 100)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskolos palūkanos turi sudaryti nuo 0 iki 100%!");
                    return 1;
                }
                else if (this.pastato_amzius > 40)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Pastato amžius ne daugiau nei 40!");
                    return 1;
                }
                else if ((this.tikros_pajamos * 0.4) < this.menesineImoka())
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
                this.vaiku_skaicius = Convert.ToInt32(this.textBox7.Text);
                this.vaiko_islaikimas = Convert.ToDouble(this.textBox6.Text);
                this.seimos_pajamos = Convert.ToDouble(this.textBox8.Text);
                this.seimos_skolos = Convert.ToDouble(this.textBox9.Text);
                this.paskolos_suma = Convert.ToDouble(this.textBox10.Text);
                this.paskolos_palukanos = Convert.ToDouble(this.textBox13.Text);

                this.liekaPajamu();
                this.grazinimoLaikotarpis(paskolosTipas);
                this.sutartiesSudarymoMokestis(paskolosTipas);
                this.metinesPalukanosSuma();

                // Vaikų skaičius
                if (this.vaiku_skaicius < 0 || this.vaiku_skaicius > 10)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Vaikų skaičius gali būti nuo 0 iki 10!");
                    return 1;
                }
                else if (this.vaiko_islaikimas <= 0 && this.vaiku_skaicius > 1)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Vaiko išlaikimo išlaidos turi viršyti 0");
                    return 1;
                }
                else if (this.vaiko_islaikimas >= 1 && this.vaiku_skaicius == 0)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Jūs negalite skirti vaikų išlaikimui, nes jų neturite!");
                    return 1;
                }
                else if (this.seimos_pajamos < 0 || this.seimos_pajamos > 10000)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Šeimos pajamos turi sudaryti nuo 0 iki 30000 €");
                    return 1;
                }
                else if (this.seimos_skolos < 0 || this.seimos_skolos > 5000)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Šeimos skola turi sudaryti nuo 0 iki 15000 €");
                    return 1;
                }
                else if (this.paskolos_suma < 300 || this.paskolos_suma > 26000)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskolos suma turi būti nuo 300 iki 26000 €!");
                    return 1;
                }
                else if (this.paskolos_palukanos < 0 || this.paskolos_palukanos > 100)
                {
                    groupBox1.Hide();
                    MessageBox.Show("Paskolos palūkanos turi sudaryti nuo 0 iki 100%!");
                    return 1;
                }
                else if ((this.tikros_pajamos * 0.4) < this.menesineImoka())
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
                mokestis = this.paskolos_suma * 0.13;
                if (mokestis < 144.81)
                {
                    label22.Text = 144.81 + " €";
                    this.sutarties_pasirasimo_mokestis = 144.81;
                    return 144.81;
                }
                else
                {
                    label22.Text = mokestis + " €";
                    this.sutarties_pasirasimo_mokestis = mokestis;
                    return mokestis;
                }
            }
            if (paskolosTipas == 2)
            {
                mokestis = this.paskolos_suma / 100;
                if (mokestis < 28.96)
                {
                    label22.Text = 28.96 + " €";
                    this.sutarties_pasirasimo_mokestis = 28.96;
                    return 28.96;
                }
                else
                {
                    label22.Text = mokestis + " €";
                    this.sutarties_pasirasimo_mokestis = mokestis;
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
