import { HttpClient } from '@angular/common/http';
import { GenericService } from './generic.service';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Pedido } from '../_model/pedido.interface';


@Injectable({
  providedIn: 'root'
})
export class PedidoService extends GenericService<Pedido>{

  constructor(protected override _http: HttpClient) 
  {
    super(
      _http,
      `${environment.HOST}pedido`
    )
   }

   public GetComisionByVendedor(year : any, month : any) {
    return this._http.get<any>(`${environment.HOST}pedido/GetComisionByVendedor?year=${year}&month=${month}`);
  }

  }
