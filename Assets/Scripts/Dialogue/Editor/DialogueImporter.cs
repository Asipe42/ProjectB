using System.Collections.Generic;
using System.IO;
using NPOI.XSSF.UserModel;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Modin
{
    [ScriptedImporter(1, "xlsx")]
    public class DialogueImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var fileName = Path.GetFileNameWithoutExtension(ctx.assetPath);

            if (!fileName.StartsWith("DGS_"))
                return;
            
            List<DialogueLine> lines = new();

            using (var stream = new FileStream(ctx.assetPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var workbook = new XSSFWorkbook(stream);
                var sheet = workbook.GetSheetAt(0);

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null || row.Cells.Count < 3) continue;

                    string id = row.GetCell(0)?.ToString()?.Trim();
                    string speaker = row.GetCell(1)?.ToString()?.Trim();
                    string text = row.GetCell(2)?.ToString()?.Trim();

                    if (string.IsNullOrEmpty(id)) continue;

                    lines.Add(new DialogueLine
                    {
                        ID = id,
                        Speaker = speaker,
                        Message = text
                    });
                }
            }

            var sequence = ScriptableObject.CreateInstance<DialogueSequence>();
            sequence.ID = fileName;
            sequence.Lines = lines;

            ctx.AddObjectToAsset(fileName, sequence);
            ctx.SetMainObject(sequence);
        }
    }
}