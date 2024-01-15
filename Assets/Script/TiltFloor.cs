using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltFloor : MonoBehaviour
{
    // SerialHandler�N���X
    public SerialHandler serialHandler;
    // Arduino�ɑ��M����f�[�^�@�`��:Xnum,num,num,num,num, (X==T/F num==move_ms num...num==act_UpDown)
    private string cmds_ = "F500,1,-1,-1,1,";


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �����L�[ ���[����]
        // ���炩�ɉ�]��������
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // ��ʓ��̌X�Ε`��
            this.transform.rotation = Quaternion.Euler(0, 0, 10);
            // �V���A�����M
            serialHandler.Write(cmds_);
        }

        // �E���L�[ ���[����]
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, -10);
        }
        // ����L�[�@�s�b�`��]
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.rotation = Quaternion.Euler(10, 0, 0);
        }
        // �����L�[�@�s�b�`��]
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.rotation = Quaternion.Euler(-10, 0, 0);
        }
        // Enter�L�[�@��]���Z�b�g
        if (Input.GetKey(KeyCode.R))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
