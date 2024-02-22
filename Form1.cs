using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace FinalProject



{


    public partial class Form1 : Form
    {
        private SQLiteDBHelper dbHelper;
        private string selectedItemId = "";
        private List<MenuRecipe> recipes = new List<MenuRecipe>();
        public Form1()
        {
            InitializeComponent();
            dbHelper = new SQLiteDBHelper();
            searchBox.TextChanged += searchBox_TextChanged;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            deleteButton.Click += deleteButton_Click;
            PopulateListView();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            System.Windows.Forms.ListView.SelectedIndexCollection selected = listView1.SelectedIndices;
            // Check if an item is selected
            if (listView1.SelectedItems.Count > 0)
            {
                MenuRecipe t = (MenuRecipe)recipes[selected[0]];

                ListViewItem selectedListItem = listView1.SelectedItems[0];

                selectedItemId = t.Name;

                // Get the selected item
                ListViewItem selectedItem = listView1.SelectedItems[0];
                

                // Get the original name of the selected item
                string originalName = selectedItem.SubItems[0].Text;

                // Fetch data from the database based on the selected item's information
                MenuRecipe selectedRecipe = dbHelper.GetMenuRecipeByName(originalName);

                // Display the selected item's details in the right panel
                if (selectedRecipe != null)
                {
                    nameTextBox.Text = selectedRecipe.Name;
                    IngriTextBox.Text = selectedRecipe.Ingrid;
                    descTextBox.Text = selectedRecipe.Desc;

                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void PopulateListView()
        {
            try
            {
                List<MenuRecipe> menuRecipes = dbHelper.GetMenuRecipesData();
                recipes = menuRecipes.ToList();
                string searchTerm = searchBox.Text.ToLower();
                listView1.Items.Clear();


                foreach (MenuRecipe recipe in menuRecipes) //populate from the dbhelper
                {
                    string name = recipe.Name.ToLower();


                    if (name.Contains(searchTerm)) //check if the name matches with the searchbox.text
                    {
                        ListViewItem item = new ListViewItem(recipe.Name);
                        item.SubItems.Add(recipe.Desc);
                        item.SubItems.Add(recipe.Id.ToString());


                        listView1.Items.Add(item); //call that item
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void imagePictureBox_Click(object sender, EventArgs e)
        {

        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string nameText = nameTextBox.Text;
            string ingriText = IngriTextBox.Text;
            string descText = descTextBox.Text;


            if(!string.IsNullOrEmpty(selectedItemId) ) 
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                dbHelper.UpdateMenuRecipe(selectedItemId, nameText, descText, ingriText);
            }




            /* if (listView1.SelectedItems.Count > 0) //if selected on list
            {

                ListViewItem selectedItem = listView1.SelectedItems[0]; //edit exist
                string originalName = selectedItem.SubItems[0].Text; //get name of the item

                //columns update
                for (int i = 0; i < selectedItem.SubItems.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            selectedItem.SubItems[i].Text = nameText;
                            break;
                        case 1:
                            selectedItem.SubItems[i].Text = descText;
                            break;
                        case 2:
                            selectedItem.SubItems[i].Text = ingriText;
                            break;


                    }
                }

                dbHelper.UpdateMenuRecipe(originalName, nameText, descText, ingriText);
                selectedItemId = "";
            }
            */
            
            else
            {
                dbHelper.InsertMenuRecipe(nameText, descText, ingriText);
                selectedItemId = "";
            }

            if (nameText.Length <= 0)
            {
                MessageBox.Show("You cannot leave empty column");
                return;
            }

            PopulateListView(); //refresh

           RemoveAllData();



        }


        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rightPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            PopulateListView();
        }

        private void IngriTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        //seçilen itemi al, ismini bul, listviewden sil ve dbden sil.
        private void deleteButton_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string nameToDelete = selectedItem.SubItems[0].Text;
                listView1.Items.Remove(selectedItem);
                dbHelper.DeleteMenuRecipeByName(nameToDelete);
            }

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
           RemoveAllData();
        }

        private void RemoveAllData()
        {
            nameTextBox.Clear();
            IngriTextBox.Clear();
            descTextBox.Clear();

            selectedItemId = "";
        }
    }
}

