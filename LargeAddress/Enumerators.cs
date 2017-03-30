using System;

namespace LargeAddress
{
    public enum AppMode : byte
    {
        Basic = 0,
        Intermediate,
        Advanced
    }

    [Flags]
    public enum Characteristics : ushort
    {
        IMAGE_FILE_RELOCS_STRIPPED = 0x0001,            // Relocation information was stripped from the file. The file must be loaded at its preferred base address. If the base address is not available, the loader reports an error.
        IMAGE_FILE_EXECUTABLE_IMAGE = 0x0002,           // The file is executable (there are no unresolved external references).
        IMAGE_FILE_LINE_NUMS_STRIPPED = 0x0004,         // COFF line numbers were stripped from the file.
        IMAGE_FILE_LOCAL_SYMS_STRIPPED = 0x0008,        // COFF symbol table entries were stripped from file.
        IMAGE_FILE_AGGRESIVE_WS_TRIM = 0x0010,          // Aggressively trim the working set. This value is obsolete as of Windows 2000.
        IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x0020,        // The application can handle addresses larger than 2 GB.
        IMAGE_FILE_BYTES_REVERSED_LO = 0x0080,          // The bytes of the word are reversed. This flag is obsolete.
        IMAGE_FILE_32BIT_MACHINE = 0x0100,              // The computer supports 32-bit words.
        IMAGE_FILE_DEBUG_STRIPPED = 0x0200,             // Debugging information was removed and stored separately in another file.
        IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP = 0x0400,    // If the image is on removable media, copy it to and run it from the swap file.
        IMAGE_FILE_NET_RUN_FROM_SWAP = 0x0800,          // If the image is on the network, copy it to and run it from the swap file.
        IMAGE_FILE_SYSTEM = 0x1000,                     // The image is a system file.
        IMAGE_FILE_DLL = 0x2000,                        // The image is a DLL file. While it is an executable file, it cannot be run directly.
        IMAGE_FILE_UP_SYSTEM_ONLY = 0x4000,             // The file should be run only on a uniprocessor computer.
        IMAGE_FILE_BYTES_REVERSED_HI = 0x8000           // The bytes of the word are reversed. This flag is obsolete.
    }
}