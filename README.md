# MMExcelTool
![image](https://user-images.githubusercontent.com/57708659/136891496-44aaa703-f6be-491b-9530-a9dfc9f72236.png)

## Description 
I know what are you thinking! Another Excel tool! There are tons already! And you are correct, but here is why those tons of tools and more could be a problem:
1. Using external tools is not always convenient!
  * Some of them use libraries that can't open some file format like (xls, csv), but only the OfficeXlm.
  * Other use ``Microsoft.Office.Interop.Excel`` that requires Exel to be installed on the machine.
  * External tool can't be customized, so eventually you have to install another one that covers your need, example I want a tool that is able to read (xls, csv, and OfficeXlm).
  * Maybe you are using a tool that I don't have, or maybe 10 more that I haven't so I have to install all of them to make the definition working.
  * Most of these tools have many other functions that maybe you don't need.
2. You can say, ok but Revit has a reader node out of the box, yep true, but if you need other functions what do you do? And also if you are one of the unlucky like me you have to re-install office 360 every time windows make an update, from dynamo [forum](https://forum.dynamobim.com/t/excel-data-importexcel-operation-failed/62197/19) ![image](https://user-images.githubusercontent.com/57708659/136896633-a69c93a9-b615-4f64-93d9-5c69d170d07e.png)
3. Grasshoper hasn't a vanilla node to read an excel file, so you will end up with the same problems described above.

Having our own library of tools allow us:
1. We can implement all the functions we need to make everyone's everyday job easier and more integrated with our workflows.
2. You don't need to install multiple libraries creating problems of usabilities across teams.
3. We maintain and improve it.

This is why I made it, and I hope we can expand with more tools, excel tool is just a little thing!

## Repository Ownership
* **Practice**: Digital Delivery
* **Sector**: Built Environment
* **Original Author(s)** [Mirco Bianchini](https://github.com/sonomirco)
* **Contact Details for Current Repository Owner(s)** mirco.bianchini@mottmac.com

## Installation Instructions
Inside the library folder:
Copy the GhExcel folder into the local folder: ``C:\Users\"your username"\AppData\Roaming\Grasshopper\Libraries``
Copy the DynExcel folder into the local folder: ``C:\Users\{USER}\AppData\Roaming\Dynamo\Dynamo {TARGET}\{VERSION}\pakages``

## Tested on
* **Rhino version**: 7
* **DynamoSandbox version**: 2.12

## Running the Code
Example files are provided into the GhExcel and DynExcel folders.

### Tags 
Grasshopper, Dynamo, MMLibrary, Excel
