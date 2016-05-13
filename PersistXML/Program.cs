using System;
using System.IO;

namespace PersistXML {
  class Program {
    static void Main( string[] args ) {



    }

    public bool ValidateFileLocation( string path ) {
      if ( File.Exists( path ) ) {
        return true;
      }

      Console.WriteLine( "No file found at {0}", path );
      return false;
    }

    public bool ValidateXMLContent(string path)
    {
      return false; 
    }
  }
}
