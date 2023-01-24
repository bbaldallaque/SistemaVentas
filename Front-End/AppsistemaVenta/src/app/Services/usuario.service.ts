import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from 'src/environment.prod';
import { ResponseApi } from '../Interfaces/response-api';
import { Login } from '../Interfaces/login';
import { Usuario } from '../Interfaces/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private urlApi: string = environment.endpoint + "Usuario/"

  constructor(private http: HttpClient) { }

 iniciarSeccion(request: Login):Observable<ResponseApi>{
  return this.http.post<ResponseApi>(`${this.urlApi}IniciarSeccion`, request)
 }

 listaUsuario():Observable<ResponseApi>{
  return this.http.get<ResponseApi>(`${this.urlApi}Lista`)
 }

 registrar(request: Usuario):Observable<ResponseApi>{
  return this.http.post<ResponseApi>(`${this.urlApi}Registrarse`, request)
 }

 editar(request: Usuario):Observable<ResponseApi>{
  return this.http.put<ResponseApi>(`${this.urlApi}Editar`, request)
 }

 eliminar(id: number):Observable<ResponseApi>{
  return this.http.delete<ResponseApi>(`${this.urlApi}Eliminar/${id}`)
 }

}