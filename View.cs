using System;
using System.Collections.Generic;

namespace hw7_mateen {
    public class View {
        public static void Print(List<Employee> employees) {
            foreach (var employee in employees) {
                Console.WriteLine(employee);
            }
        }
    }
}