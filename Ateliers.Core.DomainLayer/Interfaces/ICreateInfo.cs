using System;

namespace Ateliers
{
    /// <summary>
    /// �쐬���
    /// </summary>
    /// <remarks>
    /// �T�v: �G���e�B�e�B�̍쐬���s��ꂽ�ۂ̏���ێ����Ă��邱�Ƃ������B
    /// </remarks>
    public interface ICreateInfo
    {
        /// <summary> 
        /// �f�[�^�쐬�v���O�����̎��ʕ�������擾���܂��B
        /// </summary>
        string CreateProgramId { get; }

        /// <summary> 
        /// �f�[�^�쐬���[�U�[�̎��ʕ�������擾���܂��B
        /// </summary>
        string CreateUserId { get; }

        /// <summary> 
        /// �f�[�^�쐬�������擾�܂��͐ݒ肵�܂��B
        /// </summary>
        DateTime? CreateDateTime { get; }

        /// <summary>
        /// �G���e�B�e�B�̍쐬����ݒ肵�܂��B
        /// </summary>
        /// <param name="createProgramId"> �쐬�v���O�����̎��ʎq���w�肵�܂��B </param>
        /// <param name="createUserId"> �쐬���[�U�[�̎��ʎq���w�肵�܂��B </param>
        /// <param name="createDateTime"> �쐬���t���w�肵�܂��B </param>
        void SetCreateInfo(string createProgramId, string createUserId, DateTime createDateTime);
    }
}
