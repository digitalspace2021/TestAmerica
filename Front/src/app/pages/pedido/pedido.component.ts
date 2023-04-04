import { Component, OnInit } from '@angular/core';
import { Pedido } from 'src/app/_model/pedido.interface';
import { PedidoService } from 'src/app/_service/pedido.service';
import { Validators, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { NgbModal, NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';
import* as moment from 'moment';

@Component({
  selector: 'app-pedido',
  templateUrl: './pedido.component.html',
  styleUrls: ['./pedido.component.css']
})
export class PedidoComponent implements OnInit {

  pedidos!: Pedido[];
  pedido!: Pedido;
  form!: FormGroup;
  filter: FormControl = new FormControl("");
  
  constructor(
    private _pedidoservice: PedidoService,
    private _formBuilder: FormBuilder,
    private _modalService: NgbModal,
    private spinner : NgxSpinnerService,
    private toastr: ToastrService


  ) {
  }

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      year:[],
      month:[]
    });

    this.form
    this.GetComisionByVendedor(this.form.get('year')?.value , this.form.get('month')?.value)
  }

  operate() {
    let rawValuesForm = this.form.getRawValue();

    let pedido = {
      ...rawValuesForm
    }

    this.GetComisionByVendedor(pedido.year, pedido.month);
  }

  GetComisionByVendedor(year : any, month : any) {
    this._pedidoservice.GetComisionByVendedor(year, month).subscribe(({data}) => {
      this.pedidos = data
      console.log(this.pedidos);
      this.toastrMenssage('Comisiones obtenidas Exitosamente', 'success')
      
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
