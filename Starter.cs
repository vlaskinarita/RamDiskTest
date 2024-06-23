using System.Diagnostics;
using System.Reflection.Emit;

namespace RamDisk {
    internal class Starter {
        public static void Main(string[] args) {
            GetLetter();
            ConsoleKeyInfo ci;
            void GetLetter() {
                Console.WriteLine("Specify a letter for the new new virtual disk");
                ci = Console.ReadKey();

                try {
                    RamDrive.Mount(512, FileSystem.NTFS, (char)ci.Key, "RamDisk");
                    Console.WriteLine();
                    Console.WriteLine("For testing try copy samphing files to new drive [" + ci.Key + "]");

                }
                catch (Exception ex) {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("To delete a disk [" + ci.Key + "]  press [U]");
                    var comm = Console.ReadKey();
                    if (comm.Key == ConsoleKey.U) {
                        Unmount();
                        Console.WriteLine();
                        GetLetter();
                    }
                       
                    else
                        GetLetter();
                }
            }
            Console.WriteLine();
            Console.WriteLine("to disconnect the disk [" + ci.Key + "]  after testing, press [E]...");

            var com = Console.ReadKey();
            if (com.Key == ConsoleKey.E) {
                Unmount();
            }


            void Unmount() {
                try {
                    var fa = Directory.GetFiles(ci.Key + @":\");
                    RamDrive.Unmount((char)ci.Key);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
