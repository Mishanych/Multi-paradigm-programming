using System;
using System.Collections.Generic;
using System.IO;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string txtFile = @"D:\Downloads\input.txt";
            string[] allLines = File.ReadAllLines(txtFile);
            string[] allWords = new string[1000000];
            int[] amountOfUsedWords = new int[1000000];
            int lineIndex = 0;
            int allWordsIndex = 0;
            int amountOfUsedWordsIndex = 0;

        LoopForLines:
            if (lineIndex != allLines.Length)
            {
                string currLine = allLines[lineIndex];
                currLine += " ";
                int lengthOfLine = currLine.Length;
                string currWord = string.Empty;
                int currSymbolIndexInLine = 0;

            LoopForSymbols:
                if (currSymbolIndexInLine != lengthOfLine)
                {
                    if (currLine[currSymbolIndexInLine] != ' ')
                    {
                        if(currLine[currSymbolIndexInLine] >= 'A' && currLine[currSymbolIndexInLine] <= 'Z')
                        {
                            currWord += (char)(currLine[currSymbolIndexInLine] + 32);
                        }
                        else if(currLine[currSymbolIndexInLine] >= 'a' && currLine[currSymbolIndexInLine] <= 'z')
                        {
                            currWord += currLine[currSymbolIndexInLine];
                        }                        
                    }
                    else
                    {
                        int indexOfWord = 0;
                        int indexOfCoincidence = 0;
                        bool flag = false;

                        if (currWord.Length > 3)
                        {

                        LoopForCheckWord:
                            if (!flag && indexOfWord < allWords.Length - 1)
                            {
                                if (currWord == allWords[indexOfWord])
                                {
                                    flag = true;
                                    indexOfCoincidence = indexOfWord;
                                }
                                indexOfWord++;
                                goto LoopForCheckWord;
                            }

                            if (flag)
                            {
                                amountOfUsedWords[indexOfCoincidence]++;
                            }
                            else
                            {
                                allWords[allWordsIndex] = currWord;
                                allWordsIndex++;
                                amountOfUsedWords[amountOfUsedWordsIndex] = 1;
                                amountOfUsedWordsIndex++;
                            }
                        }
                        currWord = string.Empty;
                    }
                    currSymbolIndexInLine++;
                    goto LoopForSymbols;
                }
                lineIndex++;
                goto LoopForLines;
            }

            // Bubble sort
            int i = allWordsIndex, j = allWordsIndex, temp;
            string tempWord;

        LoopI:
            if (i >= 0)
            {
                j = allWordsIndex - 1;

            LoopJ:
                if (j >= 1)
                {
                    if (amountOfUsedWords[j] < amountOfUsedWords[j - 1])
                    {
                        temp = amountOfUsedWords[j];
                        amountOfUsedWords[j] = amountOfUsedWords[j - 1];
                        amountOfUsedWords[j - 1] = temp;

                        tempWord = allWords[j];
                        allWords[j] = allWords[j - 1];
                        allWords[j - 1] = tempWord;
                    }
                    j--;
                    goto LoopJ;
                }

                i--;
                goto LoopI;
            }

            // Print words
            i = allWordsIndex - 1;

        LoopPrint:
            if (i >= 0)
            {
                Console.WriteLine(allWords[i] + " - " + amountOfUsedWords[i]);
                i--;
                goto LoopPrint;
            }
        }
    }
}
