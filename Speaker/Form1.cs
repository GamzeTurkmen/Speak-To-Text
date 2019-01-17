using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Threading;
using System.Speech.Synthesis;
using System.Speech;
using System.Speech.AudioFormat;
namespace Speaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechSynthesizer sSynth = new SpeechSynthesizer(); //Provides access to the functionality of an installed speech synthesis engine.
        PromptBuilder pBuilder = new PromptBuilder();//Creates an empty Prompt object and provides methods for adding content, selecting voices, controlling voice attributes, and controlling the pronunciation of spoken words.
        SpeechRecognitionEngine Srecognize = new SpeechRecognitionEngine();//Provides the means to access and manage a speech recognition engine.
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        { //Speak button //Takes our sound
            pBuilder.ClearContent();
            pBuilder.AppendText(textBox1.Text); //Add selected voices to text
            sSynth.Speak(pBuilder);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //If we said word of in the below list(sList) Start button activated.And write our list.
            button2.Enabled = false;
            button3.Enabled = true;
            Choices sList = new Choices();
            sList.Add(new string[] { "hello", "experiment", "word", "flower", "beautiful", "thank you", "how are you", "today", "istanbul", "city", " I love you", "close", "exit", "open", "what is your name?" });
            Grammar gr = new Grammar(new GrammarBuilder(sList));
            try
            {
                Srecognize.RequestRecognizerUpdate();
                Srecognize.LoadGrammar(gr);
                Srecognize.SpeechRecognized += Srecognize_SpeechRecognized;
                Srecognize.SetInputToDefaultAudioDevice();
                Srecognize.RecognizeAsync(RecognizeMode.Multiple);


            }
            catch
            {
                return;
            }
        }

        public Form1(IContainer components, TextBox textBox1, Button button1, Button button2, Button button3, SpeechSynthesizer sSynth, PromptBuilder pBuilder, SpeechRecognitionEngine srecognize)
        {
            this.components = components;
            this.textBox1 = textBox1;
            this.button1 = button1;
            this.button2 = button2;
            this.button3 = button3;
            this.sSynth = sSynth;
            this.pBuilder = pBuilder;
            Srecognize = srecognize;
        }

        private void Srecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
           if (e.Result.Text=="exit")
            {
                Application.Exit();
            }
           else
            {
                textBox1.Text = textBox1.Text +  " "  + e.Result.Text.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Srecognize.RecognizeAsyncStop();
            button2.Enabled = true;
            button3.Enabled = false;
        }
    }
}

