

export const environment = {
    production: false,
    apiUrl: 'https://localhost:7017/api'
}
//private baseUrl: string = `${environment.apiUrl}/Mascota`; tambien se puede hacer de esta manera 
//y en metodo get usamos esto return this.http.get<Mascota>(`${this.baseUrl}/${id}`);