using System;
using System.Linq;
using System.Windows.Forms;

namespace Corova
{
    public partial class Form1 : Form
    {
        
        private Random rand = new Random();
        private int[] x = new int[4];
        
        private string s; // строковое представление загаданного числа
       
        private int polnoeSovpadenie; //счетчик полного совпадения
        private int chastichnoeSovpadenie; //счет частичного совпадения

        public Form1()
        {
            InitializeComponent();
            NewGame(); // вызов метода для начала новой игры
        }
        private void NewGame()
        {
            // вызов метода для генерации нового числа
            NovoeChislo();
            // обнуление метки вывода результатов сравнения загаданного и введенного чисел
            label2.Text = "";
            // обнуление метки вывода загаданного числа
            label3.Text = "";
            // открываем textbox для ввода значений
            textBox1.ReadOnly = false;
        }

        // метод генерации нового числа
        private void NovoeChislo()
        {
            bool contains; // флаг сравнения с предыдущими цифрами. совпадает - true 
            for (int i = 0; i < 4; i++)
            {
                do
                {
                    contains = false;
                    // генерация новой цифры
                    x[i] = rand.Next(10);
                    // цикл сравнения сгенерированной цифры с предыдущими
                    for (int j = 0; j < i; j++)
                        if (x[j] == x[i])
                            //если сгенериррованная цифра совпала с одной из предыдущих
                            // флаг сравнения делаем true для продолжения генерации
                            //несовпадающей цифры в элемент массива
                            contains = true;
                } while (contains);
            }
            s = x[0].ToString() + x[1] + x[2] + x[3];// из элементов массива формируем строку
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // если в текстбоксе не 4 цифры
            if (textBox1.Text.Length != 4)
            {
                // вывести сообщение об ошибке
                MessageBox.Show("Введите четырехзначное число");
            }
            else //иначе, если количество совпадений одинаковых цифр меньше 1
                if (ProverkaTextboxa() < 1) 
                    {
                        // то вызвать метод сравнения чисел
                        SravenieChisel();
                        // и вызвать метод вывода результатов сравнения на экран
                        RezultShow();
                    }
                else //иначе вывести сообщение пользователю
                    MessageBox.Show("Вы ввели повторяющиеся цифры в числе");
            // очистка текстбокса
            textBox1.Text = "";
        }

        // метод вывода результата сравнения загаданного и введенного чисел на экран
        private void RezultShow()
        {
            label2.Text += "Вы ввели: " + textBox1.Text + " : " + polnoeSovpadenie + " бык " + chastichnoeSovpadenie + " корова \n";
        }

        // метод сравнения загаданного и введенного чисел
        private void SravenieChisel()
        {
            // обнуление счетчиков
            polnoeSovpadenie = 0;
            chastichnoeSovpadenie = 0;
            // перевод содержимого текстбокса в символьный массив
            char[] ch = textBox1.Text.ToCharArray();
            // цикл проверки символов в массиве
            for (int i = 0; i < 4; i++)
            {
                // если строка s содержит в себе элемент массива
                if (s.Contains(ch[i]))
                {
                    // если номер символа в массиве совпадает с номером символа в строке
                    if (s[i] == ch[i])
                        // увеличиваем счетчик полного совпадения
                        polnoeSovpadenie++;
                    else
                        // если номер символа в массиве не совпадает с номером символа в строке
                        // увеличиваем счетчик неполного совпадения
                        chastichnoeSovpadenie++;
                }
                if (polnoeSovpadenie == 4)
                    MessageBox.Show("Число угадано!");
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = s; // передаем загаданное число
            label2.Text = "Было загадано: ";
            // запрещаем ввод символов в текстбокс
            textBox1.ReadOnly = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewGame(); // вызов метода начала новой игры
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //проверка, является ли нажатая клавиша цифрой или Backspace, возвращает true или false
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            // если были нажаты цифры, то событие обработать в обычном режиме           
                e.Handled = false;
            else
                //иначе, поставить метку что событие обработанно, но не пускать сигнал в текстбокс
                e.Handled = true;
        }

        private int ProverkaTextboxa()
        { 
            char[] ch = textBox1.Text.ToCharArray();
            int k = 0; //счетчик одинаковых чисел
            // цикл проверки одинаковых символов в массиве
            for (int i = 0; i < ch.Length - 1; i++)
            {
                for (int j = i + 1; j < ch.Length; j++)
                {
                    if (ch[i] == ch[j]) //если элемент массива I совпал с элементом массива J
                    {
                        k++; // то увеличиваем счетчик на 1
                        break; //останавливаем цикл
                    }
                }
            }
            return k; // возвращаем количество одинаковых цифр             
        }
    }
}
