using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public class CaseStudy
{
    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0) // Handle empty input
            return "[]";

        int[] maxSums = numbers.ToArray(); // Create an array of the maximum possible sums, initialized to the input values
        int maxSum = maxSums[0]; // Initialize maxSum to the first element
        for (int i = 1; i < maxSums.Length; i++) // Update the maxSums array to find the maximum increasing subarray sums
        {
            if (numbers[i] > numbers[i - 1]) // If the current number is greater than the previous one, it can extend the increasing subarray
            {
                maxSum += maxSums[i];
                maxSums[i] = maxSum;
            }
            else // Otherwise, reset maxSum to the current number
            {
                maxSum = maxSums[i];
            }
        }

        int max = maxSums.Max(); // Find the maximum sum in the maxSums array
        int ind = maxSums.ToList().IndexOf(max); // Find the index of the maximum sum

        int j = ind;
        List<int> answer = new List<int>(); // Reconstruct the maximum increasing subarray
        answer.Add(numbers[j]); // Start with the element at the index of the maximum sum
        while (j > 0) // Traverse backwards to find the elements of the increasing subarray
        {
            if (numbers[j] > numbers[j - 1]) // If the current number is greater than the previous one, it is part of the increasing subarray
            {
                answer.Add(numbers[j-1]);
                j--;
            }
            else // Otherwise, stop the traversal
            {
                j = 0;
            }

        }
        answer.Reverse(); // Reverse the list to get the correct order
        return JsonSerializer.Serialize(answer); // Serialize the result as JSON
    }
    
    
}
