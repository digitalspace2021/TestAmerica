import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/_model/item.interface';
import { ItemService } from 'src/app/_service/item.service';
import { Validators, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { NgbModal, NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {

  items!: Item[];
  item!: Item;
  form!: FormGroup;
  filter: FormControl = new FormControl("");
  
  fechaInicial : '2013/01/01 02:24:00' | undefined;
  fechaFinal : '2019/03/27 16:48:00' | undefined;

  constructor(
    private _itemservice: ItemService,
    private _formBuilder: FormBuilder,
    private _modalService: NgbModal,
    private spinner : NgxSpinnerService,
    private toastr: ToastrService
  ) {
  }

  ngOnInit(): void {
    this.GetSalesByDepartamento(this.fechaInicial, this.fechaFinal)
  }

  operate() {
    let rawValuesForm = this.form.getRawValue();

    let item = {
      ...rawValuesForm
    }

    this.GetSalesByDepartamento(item.fechaInicial, item.fechaFinal);
  }

  GetSalesByDepartamento(fechaInicial : any, fechaFinal : any) {
    this._itemservice.GetSalesByDepartamento(fechaInicial, fechaFinal).subscribe(({data}) => {
      this.items = data
      console.log(this.items);
      this.toastrMenssage('ciudades obtenidos Exitosamente', 'success')
      
    });
  }

  toastrMenssage(Mensage : string , estado : "error" | "success"){
    if(estado ==  "success") {
      this.toastr.success( `${Mensage}` , 'Mensaje del sistema!',{
        positionClass: 'toast-bottom-right',
      });
    }else{
      this.toastr.error( `Ocurrio un error` , 'Mensaje del sistema!',{
        positionClass: 'toast-bottom-right',
      });
    }
  }

}
