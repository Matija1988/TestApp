﻿using AutoMapper;
using Project.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Mapping
{
    /// <summary>
    /// Implementacija IMapping interfacea
    /// Implementation of IMapping interface
    /// </summary>
    public class MapperConfig : IMapping
    {

        /// <summary>
        /// Uzima VehicleMake i mappira ga u DTO
        /// Takes VehicleMake and maps it into DTO
        /// </summary>
        /// <returns></returns>

        public async Task<Mapper> VehicleMakerMapReadToDTO()
        {
            var mapper = await ReturnReadToDTO();
            return mapper;

        }

        /// <summary>
        /// Uzima DTO i mapira ga u VehicleMake 
        /// Takes DTO and maps it into VehicleMake
        /// </summary>
        /// <returns></returns>

        public async Task<Mapper> VehicleMakerUpdateFromDTO()
        {
            var mapper = await ReturnInsertFromDTO();
            return mapper;
        }

        /// <summary>
        /// Uzima u VehicleMake i mapira ga u DTO READ bez ID
        /// Takes the VehicleMake and maps into a DTO READ type without the ID
        /// </summary>
        /// <returns></returns>
        public async Task<Mapper> VehicleMakerDataOfUpdatedEntity()
        {
            var mapper = await ReturnUpdatedMakerData();
            return mapper;
        }

        /// <summary>
        /// Uzima Model i mapira ga u DTO
        /// Takes the model and maps it into DTO
        /// </summary>
        /// <returns>mapper</returns>

        public async Task<Mapper> VehicleModelMapReadToDTO()
        {
            var mapper = await ReturnModelReadToDTO();
            return mapper;
        }
        /// <summary>
        /// Uzima DTO i mapira ga u model
        /// Takes DTO and maps it into the model
        /// </summary>
        /// <returns>mapper</returns>
        public async Task<Mapper> VehicleModelInsertFromDTO()
        {
            var mapper = await ReturnInsertModelFromDTO();
            return mapper;
        }


        /// <summary>
        /// Uzima model i mapira ga u DTO READ bez ID
        /// Takes a model and maps it into DTO READ without ID
        /// </summary>
        /// <returns></returns>
        public async Task<Mapper> VehicleModelDataOfUpdatedEntity()
        {
            var mapper = await ReturnUpdatedModelData();
            return mapper;
        }

        private async Task<Mapper> ReturnUpdatedModelData()
        {
            return new Mapper(
                new AutoMapper.MapperConfiguration(c =>
                c.CreateMap<VehicleModel, VehicleModelDTOReadWithoutID>().ConstructUsing
                (e => new VehicleModelDTOReadWithoutID(
                    e.Name,
                    e.Abrv,
                    e.Make.Id
                    ))));
        }

        private async Task<Mapper> ReturnInsertModelFromDTO()
        {
            return new Mapper(
                new AutoMapper.MapperConfiguration(c =>
                c.CreateMap<VehicleModelDTOInsert, VehicleModel>()));

        }
        private async Task<Mapper> ReturnUpdatedMakerData()
        {
            return new Mapper(
                new AutoMapper.MapperConfiguration(c =>
                c.CreateMap<VehicleMake, VehicleMakeDTOReadWithoutID>()));
        }


        /// <summary>
        /// Enkapsulacija mapper konfiguracije koja VehicleModel
        /// model mapira na VehicleModelDTORead
        /// Encapsulation for mapper configuration which 
        /// maps VehicleModel model to DTO
        /// </summary>
        /// <returns></returns>

        private async Task<Mapper> ReturnModelReadToDTO()
        {
            var mappper = new Mapper(
                new AutoMapper.MapperConfiguration(c =>
                c.CreateMap<VehicleModel, VehicleModelDTORead>()
                .ConstructUsing(e =>
                new VehicleModelDTORead(
                    e.Id,
                    e.Name,
                    e.Abrv,
                    e.Make.Abrv
                    ))));

            return mappper;
        }
        /// <summary>
        /// Enkapsulacija mapper konfiguracije koja VehicleMake
        /// model mapira na VehicleMakeDTORead
        /// Encapsulation for mapper configuration which 
        /// maps VehicleMake model to DTO
        /// </summary>
        /// <returns></returns>

        private async Task<Mapper> ReturnReadToDTO()
        {
            var mapper = new Mapper(
                 new AutoMapper.MapperConfiguration(c =>
                 c.CreateMap<VehicleMake, VehicleMakeDTORead>()));

            return mapper;
        }
        /// <summary>
        /// Enkapsulacija mapper konfigiracije kojom se ulazni
        /// VehicleMakeDTOInsert mapira u model
        /// Encapsulation of mapper config that takes the 
        /// input DTO and maps it to model
        /// </summary>
        /// <returns></returns>
        private async Task<Mapper> ReturnInsertFromDTO()
        {
            var mapper = new Mapper(
               new AutoMapper.MapperConfiguration(c =>
               c.CreateMap<VehicleMakeDTOInsert, VehicleMake>()));

            return mapper;
        }


    }
}
