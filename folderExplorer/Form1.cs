using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace folderExplorer
{
    public partial class Form1 : Form
    {
        string path;
        public Form1()
        {
            InitializeComponent();
            path = @"D:\study\online courses";
            TreeNode root = new TreeNode();
            root.Text = path;
            treeView1.ImageList = imageList1;
            treeView1.SelectedImageIndex = 1;
            treeView1.Nodes.Add(root);
            explorer(root);
        }

        private void explorer(TreeNode node)
        {
            #region getfile
            // try get file list
            try
            {
                string[] fileList = Directory.GetFiles(node.Text);
                //MessageBox.Show(fileList.Length.ToString());
                foreach (string file in fileList)
                {
                    //get icon of the file
                    Icon icon1 = Icon.ExtractAssociatedIcon(file);
                    //add the icon to the imageList1 control
                    imageList1.Images.Add(icon1);
                    //set the treeview imageList is out imageList1
                    treeView1.ImageList = imageList1;
                    //create a new node for file and add attribute to it: text is file path and image is the icon just added
                    TreeNode fileNode = new TreeNode() { Text = file, ImageIndex = imageList1.Images.Count - 1};
                    node.Nodes.Add(fileNode);
                    //imageList1.Images.RemoveAt(imageList1.Images.Count);
                   
                }
            }
            catch
            {
                new FileNotFoundException();
            }

            #endregion getfile
            // try get folderlist
            try
            {
                

                System.IO.DirectoryInfo folder = new DirectoryInfo(node.Text);
                DirectoryInfo[] folderList = folder.GetDirectories();
                
                foreach (var item in folderList)
                {

                    

                    TreeNode Node = new TreeNode() { Text = item.FullName, ImageIndex = 0 };
                    node.Nodes.Add(Node);
                    explorer(Node);
                }
            }
            catch
            {
                return;
            }



        } //end of explorer method

       

        private void treeView1_NodeMouseDoubleClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                // Look for a file extension.
                if (e.Node.Text.Contains("."))
                    System.Diagnostics.Process.Start(e.Node.Text);
            }
            // If the file is not found, handle the exception and inform the user.
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("File not found.");
            }
        }
    }
}
