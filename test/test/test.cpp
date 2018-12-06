#include<stdio.h>
int count = 0;
int count_s = 0;
int N, M = 0;
void ignorance(char *T, char *P, int N, int M);
void KMP(char *T, char *P, int N, int M);
#define MAX 1000000

int main() {
	char T[MAX];
	char P[100];


	//   printf("두정수를 입력하시오: N(T의 길이), M(P의 길이) ");
	scanf("%d %d", &N, &M);
	if (M <= 0 && M > 100)
	{
		printf("error");
	}
	else if (N<M && N>MAX)
	{
		printf("error");
	}
	//   printf("문자열 T를 입력하시오");
	scanf("%s", &T);

	//   printf("문자열 P를 입력하시오");
	scanf("%s", &P);

	ignorance(T, P, N, M);
	KMP(T, P, N, M);

	return 0;
}

void ignorance(char *T, char *P, int N, int M) {
	int num = 0;
	int n = 0;
	int i = 0;
	int num_s = 0;

	for (n = 0; n < N + M - 1; n++) {

		if (i == M)
		{
			printf(" 문자열의 끝. 탐색 끝!");
			break;
		}
		else if (T[n] == P[i]) {

			i++;
			count++;
		}
		else if (T[n] == P[M - 1])
		{
			num++;
		}
		else
		{
			count++;
			i = i - 1;
			n = n - i;
			i = 0;
		}
	}
	printf("%d %d", num, count);
}

void KMP(char *T, char *P, int N, int M)
{
	int n, i = 0;
	int num_s = 0;

	int fail[10000] = { 0 };
	int t; int k = 0;

	for (t  = 1, k = 0; t<M; t++)
	{
	   while (k > 0 && P[t] != P[k])
		  {
			 fail[t - 1];
		  }
		  if (P[t] = P[k])
		  {
			 fail[t] = ++k;
		  }
	}
	for (n = 0; n < N + M - 1; ) {

		if (i == M)
		{
			printf(" 문자열의 끝. 탐색 끝!");
			break;
		}
		else if (T[n] == P[i]) {

			i++;
			count_s++;
		}
		else if (T[n] == P[M - 1])
		{
			num_s++;
		}
		else if (T[n] != P[i])
		{
			count_s++;

			n = n + (i - k + 1);
			i = 0;
		}

	}

	printf("%d %d", num_s, count_s);
}

/*
   //여기까지가 무식한 방식의 알고리즘


   //여기 부터가 KMP알고리즘 // 여기부터 문제

   //실패함수 구하기


   for (t = 1, k = 0; t <M; t++)
   {
	  while (k > 0 && P[t] != P[k]) fail[t - 1];
	  if (P[t] = P[k]) fail[t] = ++k;

	  //실패함수 구함
				  //여기부터  kmp안에서 문자열찾기

	  for (n = 0; n < N + M - 1; ) {

		 if (i == M)
		 {
			printf(" 문자열의 끝. 탐색 끝!");
			break;
		 }
		 else if (T[n] == P[i]) {

			i++;
			count_s++;
		 }
		 else if (T[n] == P[M - 1])
		 {
			num_s++;
		 }
		 else
		 {
			count_s++;

			n = n + (i - k + 1);
			i = 0;
		 }

	  }
   }
   printf("%d %d", num_s, count_s);

   return 0;

}
*/