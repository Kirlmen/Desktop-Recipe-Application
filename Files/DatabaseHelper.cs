using FinalProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

public class SQLiteDBHelper
{
    private string connectionString = @"Data Source=C:\\Programming\\OOPFinal\\recipes.db;Version=3;";

    public MenuRecipe GetMenuRecipeByName(string name)
    {
        string query = "SELECT * FROM MenuRecipes WHERE Name = @Name;";
        SQLiteParameter parameter = new SQLiteParameter("@Name", name);

        DataTable dataTable = ExecuteQuery(query, parameter);

        if (dataTable.Rows.Count > 0)
        {
            DataRow row = dataTable.Rows[0];

            // Create a MenuRecipe object and populate its properties
            MenuRecipe selectedRecipe = new MenuRecipe
            {
                Name = Convert.ToString(row["Name"]),
                Ingrid = Convert.ToString(row["Ingrid"]),
                Desc = Convert.ToString(row["Desc"]),
                
                // Adjust the order of properties based on your requirement
            };

            return selectedRecipe;
        }

        return null;
    }

    public void ExecuteNonQuery(string query, params SQLiteParameter[] parameters)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }
    }

    public DataTable ExecuteQuery(string query, params SQLiteParameter[] parameters)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }

 
   public void UpdateMenuRecipe(string originalName, string newName, string newDesc, string newIngrid)
     {
            string updateQuery = "UPDATE MenuRecipes SET Name = @NewName, Desc = @NewDesc, Ingrid = @NewIngrid WHERE Name = @OriginalName;";

            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
            new SQLiteParameter("@NewName", newName),
            new SQLiteParameter("@NewDesc", newDesc),
           
            new SQLiteParameter("@NewIngrid", newIngrid),
            new SQLiteParameter("@OriginalName", originalName)
            };

            ExecuteNonQuery(updateQuery, parameters);
    }
    


    public List<MenuRecipe> GetMenuRecipesData()
    {
        string selectQuery = "SELECT * FROM MenuRecipes;";
        DataTable dataTable = ExecuteQuery(selectQuery);

        List<MenuRecipe> menuRecipes = new List<MenuRecipe>();

        foreach (DataRow row in dataTable.Rows)
        {
            MenuRecipe recipe = new MenuRecipe
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = Convert.ToString(row["Name"]),
                Desc = Convert.ToString(row["Desc"]),
                Ingrid = Convert.ToString(row["Ingrid"])
            };

            menuRecipes.Add(recipe);
        }

        return menuRecipes;
    }

    public void InsertMenuRecipe(string name, string desc, string ingrid)
    {
        string insertQuery = "INSERT INTO MenuRecipes (Name, Desc, Ingrid) VALUES (@Name, @Desc, @Ingrid);";

        SQLiteParameter[] parameters = new SQLiteParameter[]
        {
            new SQLiteParameter("@Name", name),
            new SQLiteParameter("@Desc", desc),
            new SQLiteParameter("@Ingrid", ingrid)
        };

        ExecuteNonQuery(insertQuery, parameters);
    }

    public void DeleteMenuRecipeByName(string name)
    {
        string deleteQuery = "DELETE FROM MenuRecipes WHERE Name = @Name;";
        SQLiteParameter parameter = new SQLiteParameter("@Name", name);

        ExecuteNonQuery(deleteQuery, parameter);
    }
}
