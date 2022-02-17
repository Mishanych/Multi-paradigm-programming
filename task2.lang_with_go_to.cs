using System;
using System.IO;

namespace Lab1_Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            const string txtFile = @"D:\Downloads\input.txt";
            var chars = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] allLines = File.ReadAllLines(txtFile);
            string[] allWords = new string[1000000];
            string[] listOfPagesForEveryWord = new string[1000000];
            int[] numberOfPagesInListOfPages = new int[1000000];
            int lineIndex = 0;
            int allWordsIndex = 0;

        LoopForLines:
            if (lineIndex != allLines.Length)
            {
                string currLine = allLines[lineIndex];
                currLine += " ";
                int lengthOfLine = currLine.Length;
                string currWord = string.Empty;
                int currSymbolIndexInLine = 0;
                if (currLine != " ")
                {
                LoopForSymbols:
                    if (currSymbolIndexInLine != lengthOfLine)
                    {
                        if (currLine[currSymbolIndexInLine] != ' ')
                        {
                            if (currLine[currSymbolIndexInLine] >= 'A' && currLine[currSymbolIndexInLine] <= 'Z')
                            {
                                currWord += (char)(currLine[currSymbolIndexInLine] + 32);
                            }
                            else if (currLine[currSymbolIndexInLine] >= 'a' && currLine[currSymbolIndexInLine] <= 'z')
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

                                if (numberOfPagesInListOfPages[indexOfCoincidence] + 1 <= 100)
                                {
                                    if (flag)
                                    {
                                        int pageNumber = lineIndex / 45;
                                        string pageNumberToString = "";

                                    ConvertIntToString:
                                        if(pageNumber > 0)
                                        {
                                            pageNumberToString = chars[pageNumber % 10] + pageNumberToString;
                                            pageNumber /= 10;
                                            goto ConvertIntToString;
                                        }

                                        // check if pageNumber already exists in listOfPagesForEveryWord
                                        int indexInPageNumberToString = 0;
                                        bool exist = true;

                                    CheckExistance:
                                        if(indexInPageNumberToString < pageNumberToString.Length)
                                        {
                                            int startSymbolToCheck = listOfPagesForEveryWord[indexOfCoincidence].Length - pageNumberToString.Length;
                                            if (listOfPagesForEveryWord[indexOfCoincidence][startSymbolToCheck] != pageNumberToString[indexInPageNumberToString])
                                            {
                                                exist = false;
                                                indexInPageNumberToString = pageNumberToString.Length;
                                            }
                                            indexInPageNumberToString++;
                                            goto CheckExistance;
                                        }
                                        if (!exist)
                                        {
                                            listOfPagesForEveryWord[indexOfCoincidence] += ", " + pageNumberToString;
                                        }
                                    }
                                    else
                                    {
                                        allWords[allWordsIndex] = currWord;                                        
                                        listOfPagesForEveryWord[allWordsIndex] += " - " + (int)(lineIndex / 45);
                                        allWordsIndex++;
                                    }
                                    numberOfPagesInListOfPages[allWordsIndex]++;
                                }
                                else
                                {
                                    // remove word and its list
                                    allWords[allWordsIndex] = null;
                                    listOfPagesForEveryWord[indexOfCoincidence] = null;
                                }
                            }
                            currWord = "";
                        }
                        currSymbolIndexInLine++;
                        goto LoopForSymbols;
                    }
                }
                lineIndex++;
                goto LoopForLines;
            }

            // Bubble sort
            int i = 0, j = 0, index = 0, indexOfCommonSymbols = 0;
            string tempWord, temp;
            bool firstWordIsLarger = false;
        LoopI:
            if (i < allWordsIndex)
            {
                j = 0;
            LoopJ:
                if (j < allWordsIndex - 1)
                {
                    index = 0;
                    indexOfCommonSymbols = 0;
                    int lengthOfShortestWord = allWords[j].Length <= allWords[j + 1].Length ? allWords[j].Length : allWords[j + 1].Length;
                LoopForSymbolsInCurrWord:
                    if (index < lengthOfShortestWord)
                    {
                        if (allWords[j][index] > allWords[j + 1][index])
                        {
                            firstWordIsLarger = true;
                            goto EndComparingWords;
                        }
                        else if (allWords[j][index] < allWords[j + 1][index])
                        {
                            firstWordIsLarger = false;
                            goto EndComparingWords;
                        }
                        else
                        {
                            indexOfCommonSymbols++;
                            if(indexOfCommonSymbols == lengthOfShortestWord)
                            {
                                firstWordIsLarger = true;
                                goto EndComparingWords;
                            }
                        }
                        index++;
                        goto LoopForSymbolsInCurrWord;
                    }
                EndComparingWords:
                    if (firstWordIsLarger)
                    {
                        temp = listOfPagesForEveryWord[j];
                        listOfPagesForEveryWord[j] = listOfPagesForEveryWord[j + 1];
                        listOfPagesForEveryWord[j + 1] = temp;

                        tempWord = allWords[j];
                        allWords[j] = allWords[j + 1];
                        allWords[j + 1] = tempWord;
                    }
                    j++;
                    goto LoopJ;
                }

                i++;
                goto LoopI;
            }
            
            i = 0;

        LoopPrint:
            if (i < allWordsIndex)
            {
                Console.WriteLine(allWords[i] + listOfPagesForEveryWord[i]);
                i++;
                goto LoopPrint;
            }
        }
    }
}
