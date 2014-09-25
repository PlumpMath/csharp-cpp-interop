#include <stdio.h>
#include <memory.h>
#include <string.h>

extern "C"
{

int foo_1(void)
{
    printf("Hello World!\n");
    return 0;
}

int foo_2(int a, const char* b)
{
    printf("a: %d,b: %s\n", a, b);
    return 0;
}

#pragma pack(1)
struct unmanaged
{
    unsigned int a;
    unsigned int b;
    unsigned int c;
};
#pragma pack()

int foo_3(const void* managed)
{
    unmanaged* st = (unmanaged*)managed;
    printf("a: %u, b: %u, c: %u\n", 
        st->a, st->b, st->c);
    return 0;
}

int foo_4(const void* dataPtr, int length)
{
    unsigned char face[32];
    memcpy(face, dataPtr, length);

    for (int i=0; i<2; ++i)
    {
        for (int j=0; j<3; ++j)
        {
            printf("%c ", face[i*3+j]);
        }
        printf("\n");
    }

    return 0;
}

int foo_5(void** dataPtr, int* length)
{
    static unsigned char face[] = {
        '0','.','0',
        ' ','_',' ',
    };

    *dataPtr = face;
    *length = strlen((const char*)face);

    return 0;
}

int foo_6(void** dataPtr, int* length)
{
    static char *xml = 
    "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
    "<root>"
    "<node>hello world</node>"
    "<node>this is xml text</node>"
    "</root>";

    *dataPtr = xml;
    *length = strlen((const char*)xml);

    return 0;
}

int foo_7(int a)
{
    return 0;
}

int foo_8(const char* b)
{
    return 0;
}

int foo_9(const void* dataPtr, int length)
{
    return 0;
}

int foo_10(void ** dataPtr, int* length)
{
    static char buffer[1024];
    *dataPtr = buffer;
    *length = sizeof(buffer);

    return 0;
}

}
