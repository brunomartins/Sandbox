# Excel Tool
![image](https://user-images.githubusercontent.com/57708659/136891496-44aaa703-f6be-491b-9530-a9dfc9f72236.png)

I know what are you thinking! Another Excel tool! There are tons already! And you are correct, but here is why those tons of tools and more could be a problem:
* Using external tools is not always convenient!
  1. Some of them use libraries that can't open some file format like (xls, csv), but only the OfficeXlm.
  2. Other use ``Microsoft.Office.Interop.Excel`` that requires Exel to be installed on the machine.
  3. External tool can't be customized, so eventually you have to install another one that covers your need, example I want a tool that is able to read (xls, csv, and OfficeXlm).
  4. Maybe you are using a tool that I don't have, or maybe 10 more that I haven't so I have to install all of them to make the definition working.
  5. Most of these tools have many other functions that maybe you don't need.
* You can say, ok but Revit has a reader node out of the box, yep true, but if you need other functions what do you do? And also if you are one of the unlucky like me you have to re-install office 360 every time windows make an update, from dynamo [forum](https://forum.dynamobim.com/t/excel-data-importexcel-operation-failed/62197/19) ![image](https://user-images.githubusercontent.com/57708659/136896633-a69c93a9-b615-4f64-93d9-5c69d170d07e.png)
* Grasshoper hasn't a vanilla node to read an excel file, so you will end up with the same problems described above.

## Description 
The tool is composed of the follow nodes:
* ExcelReader
  1. is able to read the following formats (xls, xlsm, xlsx).
  2. is ablet to read multiple sheets, passing the name of the sheet or the index value.
  3. is able to read ranges of the sheets.
* CSVReader
  1. is able to read the csv file that for nature has only one sheet.
  2. is able to read ranges of the sheets.
* SheetNames
  1. provides the name of all the sheets present into the workbook.
* DataSheet
  1. is an object collectiong the data that will be written into a sheet.
* Writer
  1. writes an excel file.

### Tags 
Grasshopper, Dynamo, Excel
