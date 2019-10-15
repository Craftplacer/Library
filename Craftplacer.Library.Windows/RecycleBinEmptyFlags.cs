namespace Craftplacer.Library.Windows
{
	public enum RecycleBinEmptyFlags : uint
	{
		/// <summary>
		/// The recycle bin will be emptied like usual.
		/// </summary>
		None = 0x0000000,

		/// <summary>
		/// No dialog box confirming the deletion of the objects will be displayed.
		/// </summary>
		NoConfirmation = 0x00000001,

		/// <summary>
		/// No dialog box indicating the progress will be displayed.
		/// </summary>
		NoProgressUI = 0x00000002,

		/// <summary>
		/// No sound will be played when the operation is complete.
		/// </summary>
		NoSound = 0x00000004
	}
}