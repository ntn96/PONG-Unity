using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMatrix : MonoBehaviour
{
    // Autor : Antonio Serrano Miralles
    void Start()
    {
        int[,] matrix = { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 }, { 21, 22, 23, 24, 25 } };
        //int[,] matrix = { { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 } };
        //int[,] matrix = { { 1, 2},{ 1, 2 },{ 2 , 3},{ 4 , 5} };
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        Debug.Log(Spiral(matrix, rows, cols));
    }

    public string Spiral(int[,] matrix, int rows, int columns)
    {
        string result = "";

        int actualPrinted;
        int printed = 0;
        int totalSize = rows * columns;

        int iteration = 1;
        int rowIndex, colIndex, frontOffsetRow = 0, frontOffsetCol = 0, backOffsetRow = 0, backOffsetCol = 0;
        bool reverse = false;


        while (printed < totalSize)
        {
            switch (iteration)
            {
                case 1:
                    rowIndex = frontOffsetRow;
                    result += PrintRow(matrix, rowIndex, frontOffsetCol, backOffsetCol, reverse, out actualPrinted);
                    printed += actualPrinted;
                    frontOffsetRow++;
                    iteration++;
                    break;
                case 2:
                    colIndex = columns - backOffsetCol - 1;
                    result += PrintCol(matrix, colIndex, frontOffsetRow, backOffsetRow, reverse, out actualPrinted);
                    printed += actualPrinted;
                    backOffsetCol++;
                    reverse = !reverse;
                    iteration++;
                    break;
                case 3:
                    rowIndex = rows - backOffsetRow - 1;
                    result += PrintRow(matrix, rowIndex, backOffsetCol, frontOffsetCol, reverse, out actualPrinted);
                    printed += actualPrinted;
                    backOffsetRow++;
                    iteration++;
                    break;
                case 4:
                    colIndex = frontOffsetCol;
                    result += PrintCol(matrix, colIndex, backOffsetRow, frontOffsetRow, reverse, out actualPrinted);
                    printed += actualPrinted;
                    frontOffsetCol++;
                    reverse = !reverse;
                    iteration = 1;
                    break;
            }
        }
        return result;
    }

    private string PrintRow(int[,] matrix, int indexRow, int frontOffset, int backOffset, bool reverse, out int it)
    {
        string result = "";
        if (!reverse)
        {
            for (int i = frontOffset; i < matrix.GetLength(1) - backOffset; i++)
                result += matrix[indexRow, i].ToString()+",";
            it = matrix.GetLength(1) - backOffset - frontOffset;
        } else
        {
            for (int i = matrix.GetLength(1) - frontOffset - 1; i >= backOffset ; i--)
                result += matrix[indexRow, i].ToString() + ",";
            it = matrix.GetLength(1) - frontOffset - backOffset;
        }
        return result;
    }

    private string PrintCol(int[,] matrix, int indexColumn, int frontOffset, int backOffset, bool reverse, out int it)
    {
        string result = "";
        if (!reverse)
        {
            for (int i = frontOffset; i < matrix.GetLength(0) - backOffset; i++)
                result += matrix[i, indexColumn].ToString() + ",";
            it = matrix.GetLength(0) - backOffset - frontOffset;
        } else
        {
            for (int i = matrix.GetLength(0) - frontOffset - 1; i >= backOffset; i--)
                result += matrix[i,indexColumn].ToString() + ",";
            it = matrix.GetLength(0) - frontOffset - backOffset;
        }
        return result;
    }
}
