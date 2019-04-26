﻿using System.Data;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace TimeJob.ViewModel
{
  public class ImportCsv
  {
    public DataTable ReadCsv;

    public ImportCsv(string fileName, bool firstRowContainsFieldNames = true)
    {
      ReadCsv = GenerateDataTable(fileName, firstRowContainsFieldNames);
    }

    private static DataTable GenerateDataTable(string fileName, bool firstRowContainsFieldNames = true)
    {
      var result = new DataTable();

      if (fileName == "")
      {
        return result;
      }

      string delimiters = ",";
      string extension = Path.GetExtension(fileName);

      if (extension != null && extension.ToLower() == "txt")
        delimiters = "\t";
      else if (extension != null && extension.ToLower() == "csv")
        delimiters = ",";

      using (TextFieldParser tfp = new TextFieldParser(fileName))
      {
        tfp.SetDelimiters(delimiters);

        // Get The Column Names
        if (!tfp.EndOfData)
        {
          string[] fields = tfp.ReadFields();

          for (int i = 0; i < fields.Count(); i++)
          {
            if (firstRowContainsFieldNames)
              result.Columns.Add(fields[i]);
            else
              result.Columns.Add("Col" + i);
          }

          // If first line is data then add it
          if (!firstRowContainsFieldNames)
            result.Rows.Add(fields);
        }

        // Get Remaining Rows from the CSV
        while (!tfp.EndOfData)
          result.Rows.Add(tfp.ReadFields());
      }

      return result;
    }
  }
}