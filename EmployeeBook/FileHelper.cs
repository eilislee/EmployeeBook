using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace EmployeeBook
{
    static class FileHelper 
    {
        // This is the file helper that we'll use for low level methods
        // If you make a class static, you should make them all methods static
        // This is how you read and write from a file

        public static async void WriteTextFileAsync(string filename, string content) // Multi-threading is involved in this process
        {
            // Here we are creating the text file
            var localFolder = ApplicationData.Current.LocalFolder;
            var textFile = await localFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);

            // Here we are opening it in ReadWrite mode. All communications are in a form of a stream.
            var textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite);

            // On that stream we want to write to it. We use DataWriter and use the bulb to select and enable it
            var textWriter = new DataWriter(textStream);

            // Now we can finally write to the text file. We want to write a string. We want to write the content.
            textWriter.WriteString(content);

            // Here we want to finish saving our entry by using StoreAsync.
            await textWriter.StoreAsync();

            // And finally we will use Dispose to close and release the file
            textStream.Dispose();

            // We will need to find a way to append to the file though because it is currently writing over the old data!!! EL 020819
        }

        public static async Task<string> ReadTextFileAsync(string filename) // This is going to read the whole text file and return it as a string.
        {
            var localFolder = ApplicationData.Current.LocalFolder; // Access; This line allows us to access the local folder
            var textFile = await localFolder.GetFileAsync(filename); // Get file; Get file from local folder. The method we are calling from is async so we need to include await in this one. This needs to take in the filename which we've also noted.
            var textStream = await textFile.OpenReadAsync(); // Open file & create stream; Now we have the text file and need to open it and read the stream. Here we know we are only going to read it.
            var textReader = new DataReader(textStream); // Read stream; We read the stream here from the textStream.
            var textLength = textStream.Size; // Length of file; This will tell us the size of the file. Since we have the size, we can read it.
            await textReader.LoadAsync((uint)textLength); // Load String; We need to load the string before we can read from it. Also convert to uint.
            // uint keyword denotes an integral type that stores values according to the size and range 0 to 4,294,967,295. Signed 32-bit integer
            return textReader.ReadString((uint)textLength); // Return/Convert String; ReaderString will read the length 
        }
    }
}
