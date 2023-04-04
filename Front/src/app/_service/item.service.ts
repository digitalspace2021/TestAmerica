import { HttpClient } from '@angular/common/http';
import { GenericService } from './generic.service';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from '../_model/item.interface';


@Injectable({
  providedIn: 'root'
})
export class ItemService extends GenericService<Item>{

  constructor(protected override _http: HttpClient) 
  {
    super(
      _http,
      `${environment.HOST}item`
    )
   }

   public GetSalesByDepartamento(fechaInicial : any, fechaFinal : any) {
    return this._http.get<any>(`${environment.HOST}item/GetSalesByDepartamento/${fechaInicial},${fechaFinal}`);
  }

  }
