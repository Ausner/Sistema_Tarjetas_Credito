import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { ToastrService } from 'ngx-toastr';
import { TarjetaService } from 'src/app/services/tarjeta.service';


@Component({
  selector: 'app-tarjeta-credito',
  templateUrl: './tarjeta-credito.component.html',
  styleUrls: ['./tarjeta-credito.component.css']
})
export class TarjetaCreditoComponent implements OnInit {


  accion:string = "Agregar";
  id:number | undefined;
  listTarjetas:any[] = [];



  form:FormGroup;



  constructor(private fb:FormBuilder, private toastr: ToastrService, private _tarjetaService: TarjetaService) {
    this.form = this.fb.group({
      titular: ['', Validators.required],
      numeroTarjeta: ['', [Validators.required, Validators.maxLength(16), Validators.minLength(16)]],
      fechaExpiracion: ['', [Validators.required, Validators.maxLength(5), Validators.minLength(5)]],
      cvv: ['', [Validators.required, Validators.maxLength(3), Validators.minLength(3)]]
    });

   }

  ngOnInit(): void {
    this.obtenerTarjetas();
  }

  
  obtenerTarjetas(){
    this._tarjetaService.getListTarjetas().subscribe(data => {
      console.log(data);
      
      this.listTarjetas = data;
    }, error => {
      console.log(error);
    });
  }


  AgregarTarjeta(){
    const tarjeta:any = {
      titular: this.form.get('titular')?.value,
      numeroTarjeta: this.form.get('numeroTarjeta')?.value,
      fechaExpiracion: this.form.get('fechaExpiracion')?.value,
      cvv: this.form.get('cvv')?.value

    }

    
    if(this.id == undefined){
      this._tarjetaService.postTarjeta(tarjeta).subscribe(data => {
        this.toastr.success('La tarjeta fue registrada con éxito!', 'Tarjeta Registrada!');
        this.form.reset();
        this.obtenerTarjetas();
      }, error => {
        console.log(error);
      })
    }else{
      this.accion = "Agregar";
      tarjeta.id = this.id;
      this._tarjetaService.putTarjeta(this.id, tarjeta).subscribe(data => {
        this.toastr.info('La tarjeta se actualizó con exito!', 'Tarjeta Actualizada');
        this.form.reset();
        this.id = undefined;
        this.obtenerTarjetas();
      }, 
      error => {
        console.log(error);
      });
    }

    
  }

  EliminarTarjeta(id:number){

    this._tarjetaService.deleteTarjeta(id).subscribe(data => {
      this.toastr.error('La tarjeta se eliminó con exito!', 'Tarjeta Eliminada!');
      this.obtenerTarjetas();
    }, error => {console.log(error)});
    
  }

  ActualizarTarjeta(tarjeta:any){


    this.accion = "Actualizar";
    this.id = tarjeta.id;

    this.form.patchValue({
      titular: tarjeta.titular,
      numeroTarjeta: tarjeta.numeroTarjeta,
      fechaExpiracion: tarjeta.fechaExpiracion,
      cvv: tarjeta.cvv
    });
    
  
    
  }

  

}
