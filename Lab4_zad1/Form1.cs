using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace Lab4_zad1
{
    public partial class MainForm : Form
    {
        private List<string> selectedFiles = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedFiles.AddRange(ofd.FileNames);
                    foreach (var file in ofd.FileNames)
                    {
                        lstFiles.Items.Add(file);
                    }
                }
            }
        }

        private async void btnEncrypt_Click(object sender, EventArgs e)
        {
            string key = txtEncryptionKey.Text;
            string iv = txtIV.Text;

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(iv))
            {
                MessageBox.Show("Please provide an encryption key and IV.");
                return;
            }

            progressBar.Maximum = selectedFiles.Count;
            progressBar.Value = 0;

            await Task.Run(() =>
            {
                Parallel.ForEach(selectedFiles, file =>
                {
                    EncryptFile(file, key, iv);
                    Invoke(new Action(() =>
                    {
                        progressBar.Value++;
                    }));
                });
            });

            MessageBox.Show("Files encrypted successfully.");
        }

        private async void btnDecrypt_Click(object sender, EventArgs e)
        {
            string key = txtEncryptionKey.Text;
            string iv = txtIV.Text;

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(iv))
            {
                MessageBox.Show("Please provide an encryption key and IV.");
                return;
            }

            progressBar.Maximum = selectedFiles.Count;
            progressBar.Value = 0;

            await Task.Run(() =>
            {
                Parallel.ForEach(selectedFiles, file =>
                {
                    DecryptFile(file, key, iv);
                    Invoke(new Action(() =>
                    {
                        progressBar.Value++;
                    }));
                });
            });

            MessageBox.Show("Files decrypted successfully.");
        }

        private async void btnUploadFiles_Click(object sender, EventArgs e)
        {
            string serverUrl = "http://127.0.0.1:5000/upload"; 

            progressBar.Maximum = selectedFiles.Count;
            progressBar.Value = 0;

            foreach (var file in selectedFiles)
            {
                await UploadFileAsync(file + ".aes", serverUrl);
                progressBar.Value++;
            }

            MessageBox.Show("Files uploaded successfully.");
        }

        private void EncryptFile(string filePath, string key, string iv)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            int key_size = 32;
            int ivSize = 16;

            Array.Resize(ref keyBytes, key_size);
            Array.Resize(ref ivBytes, ivSize);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                using (FileStream fsCrypt = new FileStream(filePath + ".aes", FileMode.Create))
                {
                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (FileStream fsIn = new FileStream(filePath, FileMode.Open))
                            {
                                fsIn.CopyTo(cs); 

                            }
                        }
                    }
                }
            }
        }

        private void DecryptFile(string filePath, string key, string iv)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            int key_size = 32;
            int ivSize = 16;

            Array.Resize(ref keyBytes, key_size);
            Array.Resize(ref ivBytes, ivSize);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                using (FileStream fsCrypt = new FileStream(filePath + ".aes", FileMode.Open))
                {
                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (FileStream fsOut = new FileStream(filePath + "_decrypted_copy.txt", FileMode.Create))
                            {

                                cs.CopyTo(fsOut);

                            }
                        }
                    }
                }
            }
        }

        private async Task UploadFileAsync(string filePath, string url)
        {
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(File.OpenRead(filePath)), "file", Path.GetFileName(filePath));
                    var response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        private void SaveConfig(EncryptionConfig config, string filePath)
        {
            var json = JsonSerializer.Serialize(config);
            File.WriteAllText(filePath, json);
        }

        private EncryptionConfig LoadConfig(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<EncryptionConfig>(json);
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            EncryptionConfig config = new EncryptionConfig
            {
                EncryptionKey = txtEncryptionKey.Text,
                IV = txtIV.Text
            };

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveConfig(config, sfd.FileName);
                    MessageBox.Show("Configuration saved successfully.");
                }
            }
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    EncryptionConfig config = LoadConfig(ofd.FileName);
                    txtEncryptionKey.Text = config.EncryptionKey;
                    txtIV.Text = config.IV;
                    MessageBox.Show("Configuration loaded successfully.");
                }
            }
        }

        private void generateKeyIVButton_Click(object sender, EventArgs e)
        {
            using (Aes algorithm = Aes.Create())
            {
                algorithm.GenerateKey();
                txtEncryptionKey.Text = BitConverter.ToString(algorithm.Key).Replace("-", "");
                algorithm.GenerateIV();
                txtIV.Text = BitConverter.ToString(algorithm.IV).Replace("-", "");
            }

        }
    }

    public class EncryptionConfig
    {
        public string EncryptionKey { get; set; }
        public string IV { get; set; }
    }
}