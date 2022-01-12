using NPOI.SS.UserModel;
using static NPOI.SS.UserModel.CellType;

namespace SandboxCore
{
    public static class NpoiExtension
    {
        public static object GetCellValue(this IRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null)
            {
                return "null";
            }

            switch (cell.CellType)
            {
                case Numeric:
                    return cell.NumericCellValue;
                case String:
                    return cell.StringCellValue;
                case Boolean:
                    return cell.BooleanCellValue;
                case Error:
                    return cell.ErrorCellValue;
                case Formula:
                    switch (cell.CachedFormulaResultType)
                    {
                        case Blank: return "null";
                        case String: return cell.StringCellValue;
                        case Boolean: return cell.BooleanCellValue;
                        case Numeric: return cell.NumericCellValue;
                    }
                    break;

                case Blank: return "null";
            }

            return "Unknown";
        }
    }
}
