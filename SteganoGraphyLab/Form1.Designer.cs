namespace SteganoGraphyLab
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectButton = new System.Windows.Forms.Button();
            this.ipAdress = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.connectionStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.messageType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.hiddenMessage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.hiddenMessageBit = new System.Windows.Forms.TextBox();
            this.sentBits = new System.Windows.Forms.TextBox();
            this.waitingBits = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            this.messageBinary = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(281, 38);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(127, 27);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Подключиться";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click_1);
            // 
            // ipAdress
            // 
            this.ipAdress.Location = new System.Drawing.Point(12, 38);
            this.ipAdress.Name = "ipAdress";
            this.ipAdress.Size = new System.Drawing.Size(166, 27);
            this.ipAdress.TabIndex = 1;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(194, 38);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(81, 27);
            this.port.TabIndex = 2;
            // 
            // sendButton
            // 
            this.sendButton.Enabled = false;
            this.sendButton.Location = new System.Drawing.Point(743, 514);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(96, 27);
            this.sendButton.TabIndex = 3;
            this.sendButton.Text = "Отправить";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = ":";
            // 
            // connectionStatus
            // 
            this.connectionStatus.AutoSize = true;
            this.connectionStatus.Location = new System.Drawing.Point(12, 9);
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Size = new System.Drawing.Size(154, 20);
            this.connectionStatus.TabIndex = 6;
            this.connectionStatus.Text = "Статус подключения:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Передаваемый пакет";
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(12, 91);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(609, 414);
            this.messageTextBox.TabIndex = 13;
            this.messageTextBox.Text = "";
            this.messageTextBox.TextChanged += new System.EventHandler(this.messageTextBox_TextChanged_1);
            // 
            // messageType
            // 
            this.messageType.FormattingEnabled = true;
            this.messageType.Items.AddRange(new object[] {
            "HTTP/2",
            "IPv4 Header"});
            this.messageType.Location = new System.Drawing.Point(12, 511);
            this.messageType.Name = "messageType";
            this.messageType.Size = new System.Drawing.Size(404, 28);
            this.messageType.TabIndex = 14;
            this.messageType.Text = "Выберите тип пакета...";
            this.messageType.SelectedIndexChanged += new System.EventHandler(this.messageType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(422, 517);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Скрытое сообщение";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // hiddenMessage
            // 
            this.hiddenMessage.Location = new System.Drawing.Point(580, 514);
            this.hiddenMessage.Name = "hiddenMessage";
            this.hiddenMessage.Size = new System.Drawing.Size(66, 27);
            this.hiddenMessage.TabIndex = 16;
            this.hiddenMessage.TextChanged += new System.EventHandler(this.hiddenMessage_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 584);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Отправленные биты";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 616);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "Биты ожидающие отправки";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 550);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 20);
            this.label8.TabIndex = 19;
            this.label8.Text = "Скрытое сообщение биты";
            // 
            // hiddenMessageBit
            // 
            this.hiddenMessageBit.Location = new System.Drawing.Point(212, 547);
            this.hiddenMessageBit.Name = "hiddenMessageBit";
            this.hiddenMessageBit.ReadOnly = true;
            this.hiddenMessageBit.Size = new System.Drawing.Size(627, 27);
            this.hiddenMessageBit.TabIndex = 20;
            // 
            // sentBits
            // 
            this.sentBits.Location = new System.Drawing.Point(212, 581);
            this.sentBits.Name = "sentBits";
            this.sentBits.ReadOnly = true;
            this.sentBits.Size = new System.Drawing.Size(627, 27);
            this.sentBits.TabIndex = 21;
            // 
            // waitingBits
            // 
            this.waitingBits.Location = new System.Drawing.Point(212, 613);
            this.waitingBits.Name = "waitingBits";
            this.waitingBits.ReadOnly = true;
            this.waitingBits.Size = new System.Drawing.Size(627, 27);
            this.waitingBits.TabIndex = 22;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(652, 514);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(85, 27);
            this.ok.TabIndex = 23;
            this.ok.Text = "ОК";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.button1_Click);
            // 
            // messageBinary
            // 
            this.messageBinary.Location = new System.Drawing.Point(627, 91);
            this.messageBinary.Name = "messageBinary";
            this.messageBinary.ReadOnly = true;
            this.messageBinary.Size = new System.Drawing.Size(212, 414);
            this.messageBinary.TabIndex = 24;
            this.messageBinary.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(627, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Пакет в двоичном виде";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 690);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.messageBinary);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.waitingBits);
            this.Controls.Add(this.sentBits);
            this.Controls.Add(this.hiddenMessageBit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.hiddenMessage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.messageType);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.connectionStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ipAdress);
            this.Controls.Add(this.connectButton);
            this.Name = "Form1";
            this.Text = "Отправитель";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button connectButton;
        private TextBox ipAdress;
        private TextBox port;
        private Button sendButton;
        private Label label1;
        private Label connectionStatus;
        private Label label2;
        private RichTextBox messageTextBox;
        private ComboBox messageType;
        private Label label5;
        private TextBox hiddenMessage;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox hiddenMessageBit;
        private TextBox sentBits;
        private TextBox waitingBits;
        private Button ok;
        private RichTextBox messageBinary;
        private Label label3;
    }
}