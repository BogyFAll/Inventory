using System.IO;
using System.Threading.Tasks;

namespace Services
{
	/// <summary>
	/// ��� ������ � �������� ��������
	/// </summary>
	public interface IFileService
	{
		/// <summary>
		/// ���������� ��������� ���� ����������
		/// </summary>
		/// <returns></returns>
		Task<Stream> LoadFileStreamAsync();
		/// <summary>
		/// ������� ���� ����������
		/// </summary>
		void DeleteMainFile();
	}
}