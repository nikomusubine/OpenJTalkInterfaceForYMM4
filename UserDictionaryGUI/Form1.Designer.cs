namespace UserDictionaryGUI
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.UserDictionaryView = new System.Windows.Forms.DataGridView();
            this.DicViewMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DicVIewMenuStripRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.DicViewMenuStripMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.DicViewMenuStripMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UserDictionaryView)).BeginInit();
            this.DicViewMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserDictionaryView
            // 
            this.UserDictionaryView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserDictionaryView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserDictionaryView.ContextMenuStrip = this.DicViewMenuStrip;
            this.UserDictionaryView.Location = new System.Drawing.Point(12, 12);
            this.UserDictionaryView.MultiSelect = false;
            this.UserDictionaryView.Name = "UserDictionaryView";
            this.UserDictionaryView.RowHeadersWidth = 51;
            this.UserDictionaryView.RowTemplate.Height = 24;
            this.UserDictionaryView.Size = new System.Drawing.Size(776, 390);
            this.UserDictionaryView.TabIndex = 0;
            this.UserDictionaryView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.UserDictionaryView_UserDeletedRow);
            this.UserDictionaryView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserDictionaryView_KeyDown);
            this.UserDictionaryView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.UserDictionaryView_PreviewKeyDown);
            // 
            // DicViewMenuStrip
            // 
            this.DicViewMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.DicViewMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DicVIewMenuStripRemove,
            this.toolStripMenuItem2,
            this.DicViewMenuStripMoveUp,
            this.DicViewMenuStripMoveDown});
            this.DicViewMenuStrip.Name = "DicViewMenuStrip";
            this.DicViewMenuStrip.Size = new System.Drawing.Size(157, 82);
            // 
            // DicVIewMenuStripRemove
            // 
            this.DicVIewMenuStripRemove.Name = "DicVIewMenuStripRemove";
            this.DicVIewMenuStripRemove.Size = new System.Drawing.Size(156, 24);
            this.DicVIewMenuStripRemove.Text = "削除(&R)";
            this.DicVIewMenuStripRemove.Click += new System.EventHandler(this.DicVIewMenuStripRemove_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(153, 6);
            // 
            // DicViewMenuStripMoveUp
            // 
            this.DicViewMenuStripMoveUp.Name = "DicViewMenuStripMoveUp";
            this.DicViewMenuStripMoveUp.Size = new System.Drawing.Size(156, 24);
            this.DicViewMenuStripMoveUp.Text = "上に移動(&U)";
            this.DicViewMenuStripMoveUp.Click += new System.EventHandler(this.DicViewMenuStripMoveUp_Click);
            // 
            // DicViewMenuStripMoveDown
            // 
            this.DicViewMenuStripMoveDown.Name = "DicViewMenuStripMoveDown";
            this.DicViewMenuStripMoveDown.Size = new System.Drawing.Size(156, 24);
            this.DicViewMenuStripMoveDown.Text = "下に移動(&D)";
            this.DicViewMenuStripMoveDown.Click += new System.EventHandler(this.DicViewMenuStripMoveDown_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(713, 415);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "保存(&S)";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.UserDictionaryView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.UserDictionaryView)).EndInit();
            this.DicViewMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView UserDictionaryView;
        private System.Windows.Forms.ContextMenuStrip DicViewMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DicVIewMenuStripRemove;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem DicViewMenuStripMoveUp;
        private System.Windows.Forms.ToolStripMenuItem DicViewMenuStripMoveDown;
    }
}

